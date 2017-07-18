#include<iostream>
#include<queue>
#include<vector>
#include<string>
#include<climits>
#include<utility>

using std::vector;
using std::cin;
using std::cout;
using std::endl;
using std::queue;
using std::string;
using std::pair;
using std::make_pair;

void solve(vector<vector<int>>& arr, const int& n, const int& m, const vector<pair<int, int>>& oneList) {
	int cost = 0;
	
	for (size_t o = 0; o < oneList.size(); o++) {
		queue<pair<int, int>> q;
		q.push(oneList[o]);

		while (!q.empty()) {
			int i = q.front().first;
			int j = q.front().second;
			q.pop();

			int cost = arr[i][j] + 1;

			if (i > 0 && arr[i - 1][j] != 0 && arr[i - 1][ j] > cost)
			{
				arr[i - 1][j] = cost;
				q.push(make_pair(i - 1, j));
			}
			if (i < n - 1 && arr[i + 1][ j] != 0 && arr[i + 1][ j] > cost)
			{
				arr[i + 1][j] = cost;
				q.push(make_pair(i + 1, j));
			}
			if (j > 0 && arr[i][ j - 1] != 0 && arr[i][ j - 1] > cost)
			{
				arr[i][j - 1] = cost;
				q.push(make_pair(i, j - 1));
			}
			if (j < m - 1 && arr[i][ j + 1] != 0 && arr[i][ j + 1] > cost)
			{
				arr[i][j + 1] = cost;
				q.push(make_pair(i, j + 1));
			}
		}		
	}	
}

int main() {

	int t;
	cin >> t;

	while (t--) {
		int n, m;
		cin >> n >> m;

		vector<pair<int, int>> oneList;
		vector<vector<int>> arr(n);

		string line;
		for (size_t i = 0; i < n; i++) {			
			for (size_t j = 0; j < m; j++) {
				char value;
				cin >> value;

				if (value == '1') {
					arr[i].push_back(0);
					oneList.push_back(make_pair(i, j));
				}
				else {
					arr[i].push_back(INT_MAX);
				}
			}
		}

		solve(arr, n, m, oneList);

		for (size_t i = 0; i < n; i++) {
			for (size_t j = 0; j < m; j++) {
				cout << arr[i][j] << " ";
			}
			cout << endl;
		}

		string empty;
		getline(cin, empty);
		cin.ignore();
	}
	return 0;
}