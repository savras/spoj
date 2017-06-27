#include <iostream>
#include <vector>
#include <algorithm>

using std::endl;
using std::cin;
using std::cout;
using std::vector;
using std::max;

int main() {

	int t;
	cin >> t;

	for (size_t i = 0; i < t; i++) {
		int h, w;
		cin >> h >> w;

		vector<vector<int>> arr(h, vector<int>(w));
		for (size_t m = 0; m < h; m++)
		{
			for (size_t n = 0; n < w; n++)
			{
				int value;
				cin >> value;
				arr[m][n] = value;
			}
		}

		vector<vector<int>> arrDp(h, vector<int>(w));
		for (int i = 0; i < w; i++)
		{
			arrDp[0][i] = arr[0][i];
		}

		// Solve
		for (size_t row = 0; row < h - 1; row++)
		{
			for (size_t col = 0; col < w; col++)
			{
				if (col == 0)
				{
					arrDp[row + 1][col] = max(arrDp[row + 1][col], arr[row + 1][col] + arrDp[row][col]);
					arrDp[row + 1][col + 1] = max(arrDp[row + 1][col + 1], arr[row + 1][col + 1] + arrDp[row][col]);
				}
				else if (col > 0 && col < w - 1)
				{
					arrDp[row + 1][col] = max(arrDp[row + 1][col], arr[row + 1][col] + arrDp[row][col]);
					arrDp[row + 1][col + 1] = max(arrDp[row + 1][col + 1], arr[row + 1][col + 1] + arrDp[row][col]);
					arrDp[row + 1][col - 1] = max(arrDp[row + 1][col - 1], arr[row + 1][col - 1] + arrDp[row][col]);
				}
				else if (col == w - 1)
				{
					arrDp[row + 1][col] = max(arrDp[row + 1][col], arr[row + 1][col] + arrDp[row][col]);
					arrDp[row + 1][col - 1] = max(arrDp[row + 1][col - 1], arr[row + 1][col - 1] + arrDp[row][col]);
				}
			}
		}

		// Get Max from bottom row
		int result = 0;
		const int arrayOffset = 1;
		for (size_t i = 0; i < w; i++)
		{
			result = max(result, arrDp[h - arrayOffset][i]);
		}
		cout << result << endl;
	}

	return 0;
}

