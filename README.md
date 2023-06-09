# Weather API

In this project we worked as an agile team using a scrumboard with daily standups to better plan and cooperate. 
The main priority of this project was to work in a way that fits test-driven development standards and to use a CI/CD pipeline to test and deploy our work in a more fluid way.
We also heavily used branches for all the different features we developed, and had code-reviews with at least two of the team members per pull request.

## Team

<a href = "https://github.com/Chrisvasa/FoxWeatherApp/graphs/contributors">
  <img src = "https://contrib.rocks/image?repo=Chrisvasa/FoxWeatherApp"/>
</a>

[Christopher Vasankari](https://www.github.com/Chrisvasa)    |     [Jonas Wettergrund](https://www.github.com/Wettergrund)    |     [Leo Stålenhag](https://www.github.com/L-stal)    |     [Daniel Berkowicz](https://www.github.com/Berkowicz)    |          [Ilyas Kaya](https://www.github.com/AkiVonAkira)     |     [Robert Johnson](https://www.github.com/Rohnson95)

<h3>The application</h3>

We built an API with different endpoints to either get weather data about different cities, healthchecking and to see how many calls have been made since the API first started.

<h2>Tech stack</h2>
<h4>💻 Backend</h4>

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/ASP.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![xUnit](https://img.shields.io/badge/xUnit-000000?style=for-the-badge)
- Framework: .NET 6.0.
- ASP.NET Minimal API.
- xUnit

<h4>🎨 Frontend</h4>

![React](https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB)
![JS](https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=JavaScript&logoColor=white)
![SC](https://img.shields.io/badge/styled--components-DB7093?style=for-the-badge&logo=styled-components&logoColor=white)
![ESLint](https://img.shields.io/badge/eslint-3A33D1?style=for-the-badge&logo=eslint&logoColor=white)
- Built using Vite React.
- Styled with StyledComponents for CSS in JS approach.

## Getting started
### Prerequisities
You need to have .NET installed on your computer, which can be found [here](https://dotnet.microsoft.com/en-us/download "here").
Other than that you also need to make sure you have installed npm.
`npm install npm@latest -g`
### Installation
1. `git clone https://github.com/Chrisvasa/FoxWeatherApp.git`
2. Either launch the application through an IDE or build the project and then launch it using an EXE.
3. To further interact with the API you can open the `WeatherAppReactClient` folder in VSCode and type the following commands which should be enough to get you started.
`npm i`
`npm run dev`
Then either press `o` or click the link that shows up.
### Usage
The demo below shows how you can interact and retrieve weather data, favorite cities and remove favorites.
![PageDemo](README_Images/demopage.gif)

## CI/CD Pipeline
We used a simplified version of a CI/CD pipeline and this was setup with a git post-receive script using bash. And after checking if all the tests passed
then the API was deployed on the linux server.

We also added this branch to our repo so that we were able to push our changes once we merged our branches with main.
`git remote add deploy ssh://Servername:Port/home/fox/bare-FoxWeatherApp.git`

## How we worked as a team
As noted at the start of the readme, we worked by following some simple scrum methodics 
such as a scrumboard, sprints, daily standups, code reviews and pair programming.
![PullRequest](README_Images/image-2.png)
These type of reviews made it much easier to make any needed changes on new features being added to the program.
And by using these methods we greatly increased our efficiency and and people had an easier time
to know what they were supposed to work on.

## External links
![Scrumboard](https://trello.com/b/mc7PcAVI/ci-cd-team-fox)
