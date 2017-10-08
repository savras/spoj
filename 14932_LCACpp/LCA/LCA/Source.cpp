#include <iostream>
#include <vector>
#include <cmath>
#include <algorithm>

using std::endl;
using std::cin;
using std::cout;
using std::max;
using std::vector;

int GetLca(const vector<int>& parent, const vector<int>& section, const vector<int>& level, int v1, int v2)
{
	// Move to the same section for both v1 and v2.
	while (section[v1] != section[v2])
	{
		if (level[v1] > level[v2])
		{
			v1 = section[v1];
		}
		else
		{
			v2 = section[v2];
		}
	}

	// Now find common parent
	while (v1 != v2) {
		if (level[v1] > level[v2]) {
			v1 = parent[v1];
		}
		else {
			v2 = parent[v2];
		}
	}

	return v1;
}

int GetTreeHeightAndBuildParent(const vector<vector<int>>& adjList, vector<int>& parent, vector<int>& level, int currentNode, int height)
{
	level[currentNode] = height;
	if (adjList[currentNode].size() <= 0)
	{
		return height;
	}

	int maxHeight = height;
	for(size_t i = 0; i < adjList[currentNode].size(); i++)
	{
		int neighbour = adjList[currentNode][i];
		parent[neighbour] = currentNode;
		maxHeight = max(maxHeight, GetTreeHeightAndBuildParent(adjList, parent, level, neighbour, height + 1));
	}

	return maxHeight;
}

void DfsBuildSectionArray(const vector<vector<int>>& adjList, vector<int>& section, const vector<int>& parent, const vector<int>& level, int sqrtHeight, int parentOfSection, int currentNode, int currentLevel)
{
	if (level[currentNode] == 0 || level[currentNode] < sqrtHeight)
	{
		section[currentNode] = 0;
	}
	else if (level[currentNode] % sqrtHeight == 0)
	{
		section[currentNode] = parent[currentNode];
	}
	else
	{
		section[currentNode] = section[parent[currentNode]];
	}

	for (size_t i = 0; i < adjList[currentNode].size(); i++)
	{
		DfsBuildSectionArray(adjList, section, parent, level, sqrtHeight, parentOfSection, adjList[currentNode][i], currentLevel + 1);
	}
}

int main() {
	
	int testCases;
	cin >> testCases;

	// Build adjacency list
	for (size_t i = 0; i < testCases; i++)
	{		
		int n;
		cin >> n;
		vector<vector<int>> adjList(n, vector<int>());
		for (size_t j = 0; j < n; j++)
		{
			int childCount;
			cin >> childCount;
			
			for (size_t k = 0; k < childCount; k++)
			{
				int value;
				cin >> value;
				adjList[j].push_back(value - 1);
			}
		}

		vector<int> section(n);
		vector<int> parent(n);
		vector<int> level(n);
		int height = GetTreeHeightAndBuildParent(adjList, parent, level, 0, 0);
		int sqrtHeight = (int)sqrt(height);

		DfsBuildSectionArray(adjList, section, parent, level, sqrtHeight, 0, 0, 0);

		// Query
		int q;
		cin >> q;
		
		cout << "Case " << (i + 1) << ":" << endl;

		for (size_t j = 0; j < q; j++)
		{
			int v1;
			int v2;
			cin >> v1;
			cin >> v2;
			v1--;
			v2--;

			int result = 0;
			if (v1 == v2) {
				result = v1;
			}
			else {
				result = GetLca(parent, section, level, v1, v2);
			}

			cout << result + 1 << endl;
		}
	}
	return 0;
}