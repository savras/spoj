#include <stdio.h>
#include <unordered_set>

using std::unordered_set;

int main() {
	unordered_set<int> us;

	int n;
	scanf("%d", &n);
	
	int val;
	int sum = 0;
	for (size_t i = 0; i < n; i++) {
		scanf("%d", &val);
		unordered_set<int>::const_iterator it = us.find(val);
		if (it == us.end()) {
			sum += val;
			us.emplace(val);
		}
		else {
			sum -= val;
		}
	}

	printf("%d", sum);
	return 0;
}