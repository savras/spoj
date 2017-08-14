#include <iostream>
#include <vector>

using std::endl;
using std::cin;
using std::cout;
using std::vector;

int main() {
	int t;
	cin >> t;

	for (size_t i = 0; i < t; i++)
	{
		int n;
		cin >> n;
		vector<int> chemicals;
		for (size_t j = 0; j < n; j++) {
			int value;
			cin >> value;
			chemicals.push_back(value);
		}

		vector<vector<int>> mixture(n, vector<int>(n, -1));
		vector<vector<int>> smoke(n, vector<int>(n, -1));

		// Fill memoSum and memoMultiplication diagonally.
		for (size_t j = 1; j < n; j++)
		{
			int row = j - 1;
			for (int column = 0; column < n - j; column++)
			{
				row++;
				int smokeAmount = 0;
				if (mixture[row - 1][column] == -1 || mixture[row][column + 1] == -1)
				{
					mixture[row][column] = (chemicals[row] + chemicals[column]) % 100;
					smokeAmount = chemicals[row] * chemicals[column];
				}
				else
				{
					int mixtureTop = mixture[row - 1][column];
					int mixtureRight = mixture[row][column + 1];

					int topPlusCurrent = (mixtureTop + chemicals[row]) % 100;
					int rightPlusCurrent = (mixtureRight + chemicals[column]) % 100;
					if (topPlusCurrent <= rightPlusCurrent)
					{
						mixture[row][column] = topPlusCurrent;
						smokeAmount = (mixtureTop * chemicals[row]) + smoke[row - 1][column];
					}
					else
					{
						mixture[row][column] = rightPlusCurrent;
						smokeAmount = (mixtureRight * chemicals[column]) + smoke[row][column - 1];
					}
				}

				smoke[row][column] = smokeAmount;
			}
		}

		cout << smoke[n - 1][0] << endl;
	}

	return 0;
}