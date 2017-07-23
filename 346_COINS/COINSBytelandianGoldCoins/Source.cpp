#include "iostream"
#include "vector"
#include "algorithm"
#include "map"

using std::cin;
using std::cout;
using std::endl;
using std::vector;
using std::map;
using std::pair;
using std::max;
using std::min;

void bottomUpDP(int start, int end, vector<long long>& arr) {
	long sum = 0;
	for (size_t i = start; i <= end; i++) {
		long long d2 = i / 2;
		long long d3 = i / 3;
		long long d4 = i / 4;

		sum = max(arr[d2] + arr[d3] + arr[d4], (long long)i);
		arr.push_back(sum);
	}
}

long long getValue(int n, map<int, long long>& m) {
	if (n == 0) { return 0; };
	if (n == 1) { return 1; };

	map<int, long long>::iterator it;
	it = m.find(n);
	if (it != m.end()) {
		return m.at(n);
	}
	
	long long sum = 0;
	long long d2 = n / 2;
	sum += max(d2, getValue(d2, m));

	long long d3 = n / 3;
	sum += max(d3, getValue(d3, m));

	long long d4 = n / 4;
	sum += max(d4, getValue(d4, m));

	m.insert(pair<int, long long>(n, sum));

	return sum;
}

void solveDp() {
	vector<long long> arr(0);
	arr.push_back(0);	// arr[0]
	arr.push_back(0);	// arr[1]

	int t = 10, n;
	int start = 2;
	while (t--) {
		cin >> n;		
		bottomUpDP(start, n, arr);
		cout << arr[n] << endl;
		start = max(start, n + 1);
	}
}

void solveRecursive() {
	map<int, long long> m;

	int t = 10, n;
	while (t--) {
		cin >> n;	// n can be 1,000,000,000. Might cause summation to go past 2,147,xxx,xxx.
		cout << getValue(n, m) << endl;
	}
}

int main() {
	solveRecursive();
	//solveDp();
}