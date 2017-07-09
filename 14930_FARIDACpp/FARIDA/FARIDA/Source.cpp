/*
For concept behind solution, see C# project solution.

Test cases:
Input:
8
5
1 2 3 4 5
1
10
3
2 4 3
0
9
1 3 5 9 7 10 1 10 100
4
4 3 2 1
2
5 2
3
2 5 2

Output:
Case 1: 9
Case 2: 10
Case 3: 5
Case 4: 0
Case 5: 122
Case 6: 6
Case 7: 5
Case 8: 5
*/

#include <iostream>
#include <vector>
#include <algorithm>
#include <string>

using std::vector;
using std::endl;
using std::cin;
using std::cout;
using std::max;
using std::to_string;

long long solveRecursive(const vector<int>& coins, const int current, vector<long long>& memo) {
	if (current < 0) { return 0; }

	if (current >= 0 && memo[current] != 0) {
		return memo[current];
	}
	return max(solveRecursive(coins, current - 2, memo), solveRecursive(coins, current - 3, memo)) + coins[current];
}

long long solveDp(const vector<int>& coins, const int n) {
	vector<long long> dpArr;
	for (size_t j = 0; j < n; j++)
	{
		long long iMinusTwo = j > 1 ? dpArr[j - 2] : 0;
		long long iMinusThree = j > 2 ? dpArr[j - 3] : 0;

		long long maxValue = max(iMinusTwo, iMinusThree);
		dpArr.push_back(coins[j] + maxValue);
	}

	int arrayOffset = 1;	
	return max(dpArr[n - arrayOffset], dpArr[n - arrayOffset - 1]);
}

int main() {

	int t;
	cin >> t;

	for (size_t i = 0; i < t; i++)
	{
		int n;
		cin >> n;

		vector<int> coins;

		int value;
		for (size_t j = 0; j < n; j++)
		{
			cin >> value;
			coins.push_back(value);
		}

		if (n == 0)
		{
			cout << "Case " + to_string((i + 1)) + ": 0" << endl;
			continue;
		}

		if (n == 1)
		{
			cout << "Case " + to_string((i + 1)) + ": " + to_string(coins[0]) << endl;
			continue;
		}

		// Solve
		//long long result = solveDp(coins, n);

		vector<long long> memo(n);
		long long result = max(solveRecursive(coins, n - 1, memo), solveRecursive(coins, n - 2, memo));
		cout << "Case " + to_string((i + 1)) + ": " + to_string(result) << endl;

		coins.clear();
	}
}
