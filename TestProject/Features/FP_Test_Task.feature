Feature: FP_Test_Task

Scenario Outline: Create post
	Given I add post with parameters: userId '<userId>', title '<title>', body '<body>'
	Then I compare expected post data with created data
	And I check status 'Created' when post is added
	Examples:
	| userId | title      | body      |
	| Any    | Test title | Test body |

	Scenario Outline: Update post
	Given I add post with parameters: userId '<userIdCreate>', title '<titleCreate>', body '<bodyCreate>'
	And I compare expected post data with created data
	And I check status 'Created' when post is added
	When I update title '<titleUpdate>', body '<bodyUpdate>' for post
	Then I check updated post data
	And I check status 'OK' when post is updated
	Examples:
	| userIdCreate | titleCreate | bodyCreate | titleUpdate        | bodyUpdate        |
	| Any          | Test title  | Test body  | Test title updated | Test body updated |

	Scenario Outline: Binary comparison
	Given Get photo by id '<idPhoto>'
	Then Check if image of photo with id '<idPhoto>' isn't corrupted. 
	Examples:
	| idPhoto |
	| 4       |
	
