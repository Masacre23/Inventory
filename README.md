# Inventory

This test has been made trying to follow hexagonal architecture, SOLID principles and TDD.

The project is divided in 4 sections:
- Domain: Pure code, without monobehaviours. It's subdivided in 2 sections:
    - Entities: Only data classes, interfaces,... The code can't access code outside this folder.
    - UseCases: Code that implements a concrete functionality. This code only can access code inside the entities folder.
- Controllers: Here we have the InventoryController, basically it's a class that manages instances of the classes inside UseCases folder.
- Presenters: It's similar to the controllers folder but it focuses on frontend elements like UI or 3d models.
- Utils: Utilities folder, there are function extensions and common code like Injector script.

# How does it work?

The first thing to know is that if we want to use the inventory code or create another service inside UseCases, we have to add it in DependenciesInitializer and access it using Injector class. After that we can implement a new controller class, presenter, api calls,...

To create a new item we have to go inside "Resources/Data", right click and select one item inside "scriptable objects" option.

![Capture](https://user-images.githubusercontent.com/23106074/182198870-98f45907-0741-4674-a329-9d929065fa41.PNG)

In the sample scene we have a small demo. It shows an inventory where you can add items and remove them. There is a color pattern for the items.
Green is representing an item in good condition, yellow is the first state of deterioration, red is the second and black is the last one.

![Capture2](https://user-images.githubusercontent.com/23106074/182199288-68d24cc6-2e46-4a70-a29d-94c88711f5e8.PNG)
