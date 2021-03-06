#include<iostream>
#include<vector>
#include<algorithm>

using std::endl;
using std::cin;
using std::cout;
using std::vector;
using std::max;

int Travel(int h, int a, bool canGoToAir, vector<vector<int>>& cache)
{
	if (h <= 0 || a <= 0) {
		return -1;
	}

	if (cache[h][a] >= 0) {
		return cache[h][a];
	}

	if (canGoToAir)	{
		cache[h][a] = Travel(h + 3, a + 2, false, cache) + 1;
	}
	else {
		// Note that air path can be combined with this and return + 2 step instead.
		cache[h][a] = max(Travel(h - 5, a - 10, true, cache), Travel(h - 20, a + 5, true, cache)) + 1;
	}

	return cache[h][a];
}

int main() {
	int t;
	cin >> t;

	for (size_t i = 0; i < t; i++) {
		int h;
		int a;
		cin >> h >> a;

		// Assuming h = 1000 and a = 1000,		
		// Largest h = n + 3, where we increase health by 3 the very first time. Subsequent increases will never go past n + 3
		// Largest a = 1000/17 * 7 = 412, where we go by -20 health every time while increasing 7 armor
		vector<vector<int>> cache = vector<vector<int>>( h + 3 + 1, vector<int> (a + 412 + 1, -1));

		// Precompute picking of air first
		h += 3;
		a += 2;
		int result = Travel(h, a, false, cache) + 1;
		cout << result << endl;
	}

	return 0;
}