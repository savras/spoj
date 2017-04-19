#include <iostream>
#include <string>
#include <queue>
#include <stack>

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::queue;
using std::stack;

int main() {
	int t;
	cin >> t;

	int l;
	string expr;
	queue<char> queue;
	stack<char> stack;
	string result;
	while (--t >= 0) {
		result = "";

		cin >> expr;

		l = expr.length();
		int bracketCount = 0;
		for (size_t i = 0; i < l; i++) {
			/*
			if (expr[i] == '(') {
				bracketCount++;
			}
			else 
			*/
			if (expr[i] == ')') {
				while (!queue.empty())
				{
					result += queue.front();
					queue.pop();
				}

				while (!stack.empty()) {
					result += stack.top();
					stack.pop();
				}
			}
			else if (expr[i] == '+' || expr[i] == '-' || expr[i] == '*' || expr[i] == '/' || expr[i] == '^') {
				stack.push(expr[i]);
			}
			else if (expr[i] != '(') {
				queue.push(expr[i]);
			}			
		}
		cout << result << endl;
	}
}