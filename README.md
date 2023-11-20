# Database Lag Compensation Proof of Concept
## About
Welcome to the database lag compensation proof of concept repository! This proof of concept demonstrates an approach to mitigating latency in database operations. By implementing a caching mechanism, this demo enables clients to preview updated values before they are officially committed to the server. The lag compensation technique utilized here optimizes the user experience by minimizing the impact of network delays, providing a smoother and more responsive user interface.
## Key Features
* **Cache-based Data Retrieval:** Experience reduced latency as data is retrieved from a local cache before querying the database. This technique provides a faster user experience by minimizing the need for repeated server requests.
* **Asynchronous Updates:** Values are updated in the local cache before being committed to the database through asynchronous network requests. This approach allows users to see their modifications instantaneously, creating a fast and responsive user interface.
* **Lag Resistance:** By prioritizing local caching and asynchronous updates, this proof of concept showcases a resilient system that remains responsive even in the challenges of network delays.
## The Problem
Network delays can significantly impact the responsiveness of user interfaces, leading to suboptimal user experiences. An illustration of this challenge arises when users initiate updates, which involves sending requests to update the database and subsequently retrieving the updated data. This long process introduces noticeable delays, hindering the perception of real-time responsiveness to the user.

The typical workflow involves synchronous interaction with the database, where changes made by the user are committed first before any feedback is provided. As a result, users often experience lag between their actions and the reflection of those actions in the user interface, therefore degrading the overall quality of the application.

Lag not only diminishes the user experience but also constrains users, limiting their ability to work at their optimal speed. Laggy systems force users to operate at the pace limited by network delays, impeding productivity and creating frustration. This limitation is particularly pronounced in scenarios where rapid data updates are essential, or the database is at high usage, as users find themselves bound to the constraints of the laggy network.

![No Lag Compensation](https://github.com/basicn86/DatabaseLagCompensationPoC/blob/master/Images/NoLagComp.drawio.svg)

The depicted scenario showcases the inherent limitations of a laggy system, where the client is bound by the pace of the server's responses, impacting the user's ability to work efficiently.
## The Solution
Instead of waiting for a response from the server, why don't we predict the response from the server? By implementing a local cache, we allow the client to anticipate and predict server responses. When a user initiates an action, whether it be updating or retrieving data, our system utilizes the local cache first before the database. This predictive approach significantly reduces the reliance on synchronous server responses, enabling the client to operate more independently and receive virtually instantaneous feedback.

![Lag Compensation](https://github.com/basicn86/DatabaseLagCompensationPoC/blob/master/Images/LagComp.drawio.svg)

As depicted above, when a user initiates an action, whether updating or retrieving data, the system consults the local cache first. This approach ensures that the client receives real-time feedback, observing changes on the client side before any data is sent to the server. Subsequently, asynchronous network requests are dispatched to update the database, aligning the server with the locally previewed changes. This approach guarantees that the lag that the user perceives is much less than the real lag, leading to a responsive user experience.

## Demonstration Program
### Example 1
![Example 1](https://github.com/basicn86/DatabaseLagCompensationPoC/blob/master/Images/ex1.png)

In this example, we retrieve a set of messages from the database. Since the cache is empty, retrieving data from the database for the first time takes a long time. Data from the first request will be utilized to fill the caching layer, making any subsequent requests faster than the first.
### Example 2
![Example 2](https://github.com/basicn86/DatabaseLagCompensationPoC/blob/master/Images/ex2.png)

In this example, we update the contents of a set of messages from the database. When the user has updated those messages, the user can see their changes reflected immediately while the network requests for those changes are made in the background. This results in a more responsive user experience as there is no need to wait for a database response. Towards the end of the example, we verified that our changes were successfully applied to the database by waiting and retrieving the data from the database again.
