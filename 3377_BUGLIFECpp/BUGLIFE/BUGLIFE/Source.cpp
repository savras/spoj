#include<iostream>
#include<vector>
#include<queue>
#include<cmath>
#include<algorithm>
#include<string>

using std::endl;
using std::cout;
using std::cin;
using std::string;
using std::vector;
using std::queue;
using std::max;

bool solveBfs(const vector<vector<int>>& adjList, const int& bugCount, const int& interactions) {
	const int white = 1;
	const int red = 2;
	vector<int> colours(bugCount);	
	queue<int> q;

	int c = 0;
	int next = 0;
	bool isSuspicious = false;
	int lastProcessedBugNumber = -1;
	while (next != -1 && !isSuspicious && lastProcessedBugNumber < bugCount - 1)
	{
		next = ++lastProcessedBugNumber;
		q.push(next);
		colours[next] = red;
		while (q.size() > 0 && !isSuspicious)
		{
			int node = q.front(); 
			q.pop();

			for(size_t i = 0; i < adjList[node].size(); i++)
			{
				int neighbour = adjList[node][i];
				if (colours[neighbour] == 0)
				{
					lastProcessedBugNumber = max(neighbour, lastProcessedBugNumber);
					q.push(neighbour);
					colours[neighbour] = colours[node] == red ? white : red;
				}

				if (colours[node] == colours[neighbour])
				{
					isSuspicious = true;
				}
			}
		}
	}

	return isSuspicious;

}

int main() {
	int t;
	cin >> t;

	for (size_t i = 0; i < t; i++) {
		int bugCount, interactions;
		cin >> bugCount >> interactions;

		// Prepare adj List
		vector<vector<int>> adjList(bugCount);

		// Build Adj list
		for (size_t j = 0; j < interactions; j++)
		{
			int a;
			int b;
			cin >> a >> b;
			a--; b--;
			

			adjList[a].push_back(b);
			adjList[b].push_back(a);
		}

		bool result = solveBfs(adjList, bugCount, interactions);
		cout << "Scenario #" << (i + 1) <<  ":" << endl;;
		if (result) {
			cout << "Suspicious bugs found!" << endl;
		}
		else {
			cout << "No suspicious bugs found!" << endl;
		}		
	}
}