# Picross Solver

PicrossSolver is an attempt to automatically solve levels from the picross-game Hungry Cat Picross made by [Tuesday Quest][tuesdayQuest].

[![Google Play](images/google_play.png)](http://market.android.com/details?id=com.tuesdayquest.logicart)
[![App Store](images/app_store.png)](https://itunes.apple.com/us/app/hungry-cat-picross/id737744473)

The program is written purely in C# and WPF, and is currently separated into three parts: 
  - Domain: A class library where the magic happens. All the business logic is placed here.
  - DomainTests: Unit-tests and integration-tests for the Domain library.
  - PicrossSolver: WPF App for visualizing the output from the Domain library.

The solver can currently take following inputs:
  - A comma-and-newline-separated string with ARGB values for the grid.
  - A .png file where 1 pixel corresponds to one cell in the grid.

### Version
0.0.1

### Development

Want to contribute? Great!

These are some of the stuff that still remains to do:
  - The domain library probably needs some refactoring
  - Some levels are still not solveable by the solver
  - Some levels from the game are still not added as images to the project yet. Solutions for all levels exist at [picrossmadness.com][solutions]
  - Unit-tests needs to be written, not just integration-tests.

I am new to using GitHub for open-source development, but if someone submitted a pull-request I would be able to figure out how to merge it into the project.

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [tuesdayQuest]: <http://www.tuesdayquest.com/>
   [solutions]: <http://www.picrossmadness.com/index.php/hungry-cat-picross/hcp-solutions>