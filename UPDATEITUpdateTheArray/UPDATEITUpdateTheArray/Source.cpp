#include <iostream>
#include <vector>

using std::cin;
using std::vector;
using std::ios_base;

// Naive implementation.
int main() {
	int t;
	scanf("%d", &t);

	int n, u, l, r, val;
	int q, p;

	while (t--) {
		scanf("%d%d", &n, &u);
		vector<long long> arr(n);
		while (u--) {
			scanf("%d%d%d", &l, &r, &val);

			for (size_t i = l; i <= r; i++) {
				arr[i] += val;
			}
		}
		scanf("%d", &q);
		while (q--) {
			scanf("%d", &p);
			printf("%d\n", arr[p]);
		}
	}
}