#include "iostream"
#include "vector"
#include "algorithm"

using std::cin;
using std::cout;
using std::endl;
using std::vector;
using std::max;

int main() {	
	vector<long long> arr(0);

	arr.push_back(0);
	arr.push_back(1);

	int t = 10, n;
	while (t--) {
		cin >> n;	// n can be 1,000,000,000. Might cause summation to go past 2,147,xxx,xxx.

		if (n >= 0) {

			long sum = 0;
			for (size_t i = arr.size(); i <= n; i++) {
				long long d2 = i / 2;	// Don't need to be long long, but then can't use max(int, long)
				long long d3 = i / 3;
				long long d4 = i / 4;

				sum = max(d2, arr[d2]) + max(d3, arr[d3]) + max(d4, arr[d4]);
				if (arr.size() <= i) {
					arr.push_back(sum);
				}
			}

			cout << arr[n] << endl;
		}
	}
}