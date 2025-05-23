# Concept Of Random
A final project created for SE320 at Chapman University.
[Video overview here](https://www.youtube.com/watch?v=BP8EIcKVMiA)

## Separation of Responsibilities
- Each person was responsible for their own unit tests/functionality/Jira entries.
- Ivan: View / Controller / ConsoleRenderer + refactoring other people's code to work with those systems
- Joshua: all API work, TimerObserver, TimerStatus, TimerTracker, a small amount of Blackjack Refactoring (see commits). Also put together documentation, video, code coverage report.
- Maks: Bot, Coin, Dice, Pot, Player, Wallet, IRandomGenerator
- Zack: Blackjack game, narration for simulation section and writing for blackjack section of video.

## Build/Usage Instructions
- Pull the repository into Rider (or download as a ZIP if an error occurs)
- Resize the console as desired and press run!
- Navigate the menu using arrow keys, numbers, and the return/enter key.
- Tip: run in an external terminal, it's likely to work better outside of the IDE. To do so, find the bin folder and locate the executable after pressing run in Rider (which will generate a build).

## Usage Notes / Known Issues
- If any page does not render, main menu included, press one of the up/down arrow keys to re-render.
- After selecting an API call, the system may take time before displaying your response. To force a re-render, press the Enter/Return key.
- The Weather API may take a long time occasionally. This seems to be a caching issue for the API itself, as the weather calls speed up the more you've done them.

## Design Decisions, Architecture, Singleton, Observer, and Strategy.
For our project, we wanted to use real data and to design a project that wouldn't quickly get out of hand. It was supposed to be composed of two parts: local simulations, things like choosing from a list or rolling a die, and API usage, things like getting random facts or quotes. Then, we'd add another level of randomness and allow for individual elements to be randomly chosen. Basically, we wanted to perform a random operation. This would allow for a relatively easy separation of concerns; API integration would be separate from simulations, and those would compose our model.

For the view, we decided to use Console Renderer because Unity proved to be a challenge, given storage space and knowledge limitations.

### APIs
For the APIs, Joshua created a unified interface in order to have a universal `Get()` method. Each API inherits from the API Interface. We also used **Singleton** here, allowing each class to reuse the same HTTP client wrapped in the `APIClient` class. This is because there's no need to call more than one API at a time, in this particular implementation.

### Simulations
#### Blackjack
Blackjack implements the **Strategy** pattern, allowing for multiple ways of visualizing cards. It also uses Enums to avoid magic numbers.

#### Coin/Dice
Coin and Dice are relatively simple but designed for maximum testability, similarly to the API usage.

#### Dice Games
Using Dice, Maks had the idea to implement various dice games. This proved a challenge, so, while the groundwork was created, no games were implemented.

#### Timer
The timer implementation is also lumped in with the rest of the simulations, as its entirely local. Using the **Observer** pattern, there's a `TimerTracker` that's an observable and may be observed by `TimerObserver`s. In `TimerTracker`, you can add observers, start a timer to notify the observers, and remove the observers.

### UI
The UI was implemented using the console renderer, separating View and Controller into their own sections.

The controller is in charge of any logic and tells the view to display things.

## Jira Board
An export of the Jira board is available at the root level of this repository. A test coverage report in HTML form is also available.
