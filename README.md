<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/bobrandy13/webka">
    <img src="https://i.postimg.cc/Y0ZN25h3/webka.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">Webka</h3>

  <p align="center">
    Rebuilding Kafka, for the web, and making it easier to use. 
    <br />
    <a href="https://github.com/bobrandy13/webka"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/bobrandy13/webka/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    ·
    <a href="https://github.com/bobrandy13/webka/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->

## About The Project

### Description

This project is a custom implementation of a Kafka-like messaging system using HTTP, ASP.NET, C#, and Microsoft SQL
Server. The goal is to replicate the core functionalities of Apache Kafka, providing a reliable and scalable platform
for building real-time data pipelines and streaming applications.
Features

- **Cluster**: Manage multiple clusters for organizational and scalability purposes.
- **Topics**: Create and manage topics where messages are published.
- **Partitions**: Divide topics into partitions for parallel processing and load balancing.
- **Producers**: Send messages to topics, with support for key-value pairs.
- **Consumers**: Subscribe to topics and consume messages, with configurable settings like auto-commit.
- **Consumer Groups**: Group multiple consumers together to manage load balancing and fault tolerance.
- **Subscriptions**: Define which consumers are subscribed to which topics.
- **Messages**: Store and manage messages within partitions, supporting retrieval and replay.
- **HTTP Interface**: Interact with the system using a RESTful API built with ASP.NET

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->

## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/bobrandy13/webka.git
   ```
2. Start the MS SQL Server. (Working on supporting other DBs)
3. Run the migrations to create the database.
   ```zsh
   dotnet ef database update
   ```
 
4. Run the project in Visual Studio.
   ```zsh
   dotnet run
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- USAGE EXAMPLES -->

## Usage

For more examples, please refer to the swagger documentation. Linked here [here](https://www.example.com)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- ROADMAP -->

## Roadmap

## Roadmap

- [x] Set up project with ASP.NET and C#
- [x] Implement Kafka-like messaging system
- [x] Create models for Cluster, Topics, Partitions, Producers, Consumers, Consumer Groups, Subscriptions, and Messages
- [x] Implement HTTP Interface with RESTful API
- [x] Implement Producers
- [x] Implement Consumers
- [x] Implement Consumer Groups 
- [x] Implement Subscriptions
- [x] Implement Messages
- [x] Save data to MS SQL Server
- [x] Allow producers to produce messages
- [ ] Producer must specify a topic, and specify partition
- [ ] Allow all types of messages to be produced, Objects, json, strings, int, etc
- [ ] Partition producer's data into partitions using a partition key and hashing algorithms
- [ ] Have a replication factor
- [ ] Allow consumers to consume messages
- [ ] Allow consumers to specify the offset to start consuming messages
- [ ] Atleast once, at most once, and exactly once delivery semantics, allow producer to specify.
- [ ] Save data across multiple brokers
- [ ] Add support for real-time updates using WebSockets or Server-Sent Events
  - [ ] Allow consumers to connect through SSE in real-time
- [ ] Implement a system for monitoring and logging activity
- [ ] Add testing. 
- [ ] Implement authentication and authorization for API access

See the [open issues](https://github.com/bobrandy13/webka/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- CONTRIBUTING -->

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any
contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also
simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->

## License

Distributed under the MIT License.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->

## Contact

Project Link: [https://github.com/bobrandy13/webka](https://github.com/github_username/repo_name)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->

[contributors-shield]: https://img.shields.io/github/contributors/bobrandy13/webka.svg?style=for-the-badge

[contributors-url]: https://github.com/bobrandy13/webka/graphs/contributors

[forks-shield]: https://img.shields.io/github/forks/bobrandy13/webka.svg?style=for-the-badge

[forks-url]: https://github.com/bobrandy13/webka/network/members

[stars-shield]: https://img.shields.io/github/stars/bobrandy13/webka.svg?style=for-the-badge

[stars-url]: https://github.com/bobrandy13/webka/stargazers

[issues-shield]: https://img.shields.io/github/issues/bobrandy13/webka.svg?style=for-the-badge

[issues-url]: https://github.com/bobrandy13/webka/issues

[license-shield]: https://img.shields.io/github/license/bobrandy13/webka.svg?style=for-the-badge

[license-url]: https://github.com/bobrandy13/webka/blob/master/LICENSE.txt

[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555

[linkedin-url]: https://linkedin.com/in/kevinhuang31

[product-screenshot]: images/screenshot.png

[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white

[Next-url]: https://nextjs.org/

[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB

[React-url]: https://reactjs.org/

[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D

[Vue-url]: https://vuejs.org/

[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white

[Angular-url]: https://angular.io/

[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00

[Svelte-url]: https://svelte.dev/

[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white

[Laravel-url]: https://laravel.com

[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white

[Bootstrap-url]: https://getbootstrap.com

[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white

[JQuery-url]: https://jquery.com 