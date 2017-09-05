#include <stdio.h>
#include <unordered_set>

using std::unordered_set;

void solveXor(const int& n) {
	int val;
	scanf("%d", &val);
	for (size_t i = 1; i < n; i++)
	{
		int nextVal;
		scanf("%d", &nextVal);
		val ^= nextVal;
	}
	printf("%d", val);
}

void solveMap(const int& n) {
	unordered_set<int> us;
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
}

int main() {
	
	int n;
	scanf("%d", &n);
	solveXor(n);
	//solveMap(n);

	return 0;
}