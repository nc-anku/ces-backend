public static class TotalRouteSearch
{
	public static List<Route> FindTotalRoute(int from, int to, bool price) 
	{
		int[,] connections = GetConnections();
		int[,,] graph = CreateGraph(connections, price);
		List<Route> totalRoute = SearchGraph(graph, from, 32, to);
		return totalRoute;
		//return totalRoute;

	}

	private static int[,,] CreateGraph(int[,] connections, bool price)
	{
		int[,,] graph = new int[32, 32, 2];
		for (int i = 0; i < 32; i++)
		{
			for (int j = 0; j < 32; j++)
			{
				graph[i, j, 0] = int.MaxValue / 2;
				graph[i, j, 1] = -1;
			}
		}
		for (int i = 0; i < connections.GetLength(0); ++i)
		{
			int x = connections[i, 0] - 1;
			int y = connections[i, 1] - 1;
			int vendor = connections[i, 3];
			int value;
			if (price)
			{
				value = RouteUtils.GetPrice(vendor, x, y);
			} else
			{
				value = RouteUtils.GetTime(vendor, x, y);
			}
			if (value < graph[x, y, 0] && value < graph[y, x, 0])
			{
				graph[x, y, 0] = value;
				graph[y, x, 0] = value;
				graph[x, y, 1] = vendor;
				graph[y, x, 1] = vendor;
			}
		}
		return graph;
	}

    private static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
    {
        int min = int.MaxValue;
        int minIndex = 0;

        for (int v = 0; v < verticesCount; ++v)
        {
            if (shortestPathTreeSet[v] == false && distance[v] <= min)
            {
                min = distance[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    private static List<Route> GetTotalRoute(int[] prev, int from, int to, int[,,] graph)
    {
        List<int> path = new List<int>();
		List<Route> totalRoute = new List<Route>();
        if (from == to)
            return totalRoute;
        int j = to;
        while (j != from)
        {
            path.Add(j);
            int company = graph[j, prev[j], 1];
            int price = RouteUtils.GetPrice(company, j, prev[j]);
            int time = RouteUtils.GetTime(company, j, prev[j]);
            Route r = new Route { From=prev[j], To=j, Price=price, Time=time };
			totalRoute.Add(r);
            j = prev[j];
            

            //graph[j, prev[j], 1];
        }
        totalRoute.Reverse();

        return totalRoute;
    }

    public static List<Route> SearchGraph(int[,,] graph, int from, int verticesCount, int to)
    {
        int[] distance = new int[verticesCount];
        int[] prev = new int[verticesCount];

        bool[] shortestPathTreeSet = new bool[verticesCount];

        for (int i = 0; i < verticesCount; ++i)
        {
            distance[i] = int.MaxValue;
            shortestPathTreeSet[i] = false;
        }

        distance[from] = 0;

        for (int count = 0; count < verticesCount - 1; ++count)
        {
            int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
            shortestPathTreeSet[u] = true;
            if (u == to)
            {
                Console.WriteLine("ucity: {0}, previous: {1}, distance: {2}", u, prev[u], distance[u]);
                return GetTotalRoute(prev, from, to, graph);
            }

            for (int v = 0; v < verticesCount; ++v)
                if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v, 0]) && distance[u] != int.MaxValue && distance[u] + graph[u, v, 0] < distance[v])
                {
                    distance[v] = distance[u] + graph[u, v, 0];
                    prev[v] = u;
                }
        }
        Console.WriteLine("No route found from {0} to {1}", to, from);
		return new List<Route>();
    }


    private static int[,] GetConnections()
	{
		int[,] connections = {
				{1,3,2,0},
				{1,9,5,0},
				{1,8,5,0},
				{3,4,8,0},
				{4,5,4,0},
				{5,6,5,0},
				{5,7,5,0},
				{6,7,4,0},
				{7,12,5,0},
				{8,13,8,0},
				{9,10,3,0},
				{10,14,6,0},
				{12,14,3,0},
				{14,15,4,0},
				{11,13,4,0},
				{11,12,7,0},
				{11,21,6,0},
				{12,13,7,0},
				{12,21,5,0},
				{13,21,6,0},
				{13,17,2,0},
				{13,16,4,0},
				{16,18,3,0},
				{18,19,3,0},
				{19,29,6,0},
				{18,20,3,0},
				{18,20,2,0},
				{20,28,4,0},
				{20,27,6,0},
				{20,29,5,0},
				{27,29,3,0},
				{22,28,4,0},
				{21,22,3,0},
				{22,27,10,0},
				{22,32,11,0},
				{22,25,11,0},
				{25,27,4,0},
				{25,32,3,0},
				{27,32,5,0},
				{23,32,4,0},
				{23,24,4,0},
				{1,2,1,1},
				{2,5,1,1},
				{5,31,1,1},
				{24,31,1,1},
				{24,26,1,1},
				{24,25,1,1},
				{24,30,1,1},
				{19,30,1,1},
				{19,20,1,1},
				{20,25,1,1},
				{16,20,1,1},
				{24,28,1,1},
				{13,28,1,1},
				{13,16,1,1},
				{15,16,1,1},
				{23,24,1,1},
				{22,23,1,1},
				{6,23,1,1},
				{6,22,1,1},
				{3,6,1,1},
				{6,10,1,1},
				{10,13,1,1},
				{1,2,3,2},
				{2,4,5,2},
				{4,5,3,2},
				{4,31,10,2},
				{5,31,11,2},
				{5,6,4,2},
				{6,12,4,2},
				{12,23,9,2},
				{23,24,3,2},
				{24,31,9,2},
				{24,26,8,2},
				{26,27,3,2},
				{19,27,8,2},
				{19,30,8,2},
				{16,19,4,2},
				{15,16,4,2},
				{9,15,5,2},
				{1,9,3,2}
		};
		return connections;
	}
}