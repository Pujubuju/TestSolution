Feature: CalculatorDivide
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the division of two numbers

Scenario: Divide two numbers
	Given I have entered 8 into the calculator
	And I have also entered 4 into the calculator
	When I press divide
	Then the result should be 2 on the screen

	Given I have entered 8 into the calculator
	And I have also entered 0 into the calculator
	When I press divide
	Then the exception should be thrown

	Given I have entered 0 into the calculator
	And I have also entered 0 into the calculator
	When I press divide
	Then the exception should be thrown
