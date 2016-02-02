Feature: CalculatorSubtract
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the subtraction of two numbers

Scenario: Subtract two numbers
	Given I have entered 50 into the calculator
	And I have also entered 70 into the calculator
	When I press subtract
	Then the result should be -20 on the screen
