Feature: PropertyCheck
	As a QA, I like to verify Property searching feature
        and Images displayed on a property page. 

@scenario1
Scenario: Search properties
Given When I am at home page
    And selected Marina Bay Residences by searching Marina
    And floor area  to have maximum 3000 sqft
When click on search
Then all search result must have property name - Marina Bay Residences  with area less than 3000 sqft

@scenario2
Scenario: Verify Images on a property page
Given When I am at home page
    And select listing type as Rent
    And having maximum price of $15000
    And properties should have photos displayed
When click on search
Then select the first result
    And check if all images are displayed