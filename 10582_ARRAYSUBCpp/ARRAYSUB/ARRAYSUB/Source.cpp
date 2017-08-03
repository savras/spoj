#include<iostream>
#include<vector>

using std::cin;
using std::cout;
using std::endl;
using std::vector;

int main() {
	int n;
	cin >> n;

	vector<int> arr;
	int value;
	for (size_t i = 0; i < n; i++) {
		cin >> value;
		arr.push_back(value);
	}

	int k;
	cin >> k;
	
	int max;
	for (size_t i = 0; i <= n - k; i++)
	{
		max = arr[i];
		for (size_t j = i; j < i + k; j++)
		{
			if (max < arr[j])
			{
				max = arr[j];
			}
		}
		cout << max << " ";
	}

	return 0;
}