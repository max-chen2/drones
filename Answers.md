# GetSwift Code Test

## Answers to questions

### How did you implement your solution?
For each package to be delivered, the solution looks for the best candidate (drone) to deliver the package, the best candidate is the drone that delivers the package quickest.

When auctioning for the best offer for a package, the problem is simplified by not using their ETA on the package assignment but on the total distance required to travel to pick up the package from depot (including distance to deliver currently assigned package). Because the distance from the depot to destination is the same for every drone so we can ignore this. Also, the speed is constant for all drones, therefore, the actual "time" it takes can be ignored at the auction stage.

To ensure we deliver as many as possible, the hardest package is paired first with the best drone.

1. Calculate all packages by getting the distance from depot.
2. Calculate remaining seconds for each package.
3. Use distance / seconds to calculate difficulty.
The higher the difficulty, the sooner it will require a drone to deliver.

Drones are also pre-calculated with distance to depot and stored.
For each package in descending difficulty order, the best drone [0] is selected and whether the drone can deliver before deadline is evaluated. If yes, then becomes assignment, if not, package becomes unassigned.

Then the next easier package.

### Why did you implement it this way?

The distance and difficulties for drones and packages are pre-calculated, to reduce unnecessary repeated calculation.

My first approach was running through the auction process by defualt order that packages list comes in with. I always assign the best drone to the current package. The problem I had was, some packages that are easy but reserves the best drone. (this is interpreted from the requirement that a package should be assigned to the drone that delivers quickest). However, the problem with that is, less number of packages gets delivered each dispatch.

So in my final approach, i have re-arranged the packages by difficulty and this enables me to deliver more packages. (while still assigning the best and quickest drone for each package).



### Let's assume we need to handle dispatching thousands of jobs per second to thousands of drivers. Would the solution you've implemented still work? Why or why not? What would you modify? Feel free to describe a completely different solution than the one you've developed.

If delivery orders and drone numbers increase, the solution should still work, but there can be occurrences that the dispatcher system does not provide the most optimized solution. E.g. During dispatching calculation, due to the high number of packages and drones, drones can be arriving (are all drones returned from the /drones request), or becomes online? If this happens, the drone just arrived or became online should be the best candidate for any package. However, in this case, can be missed by dispatcher service.

Modifications:
1. Add a IsAtDepot property on drones to simplify. Drones arrive at depot and this property updates. This can be served by another API method e.g. /standbydrones. The drones that are returned in this request should be prioritised and assigned without auctioning.
2. Persist ETA data with drone, in addition to drone's location, on dispatching a drone to deliver, the ETA timestamp can be calculated and persisted for each drone. The eta should include delivery time + getting back to depot time. The auction process will be more proformant.
3. If operating in a large area, e.g. State, Country. Drones should be partitioned by region.
4. If drones can carry more than 1 package, routing would be a good addition.
