Feature: CalculatorMultiply
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the multiplication of two numbers

Scenario: Multiply two numbers
	Given I have entered 8 into the calculator
	And I have also entered 9 into the calculator
	When I press multiply
	Then the result should be 72 on the screen
