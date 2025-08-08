# The-Great-Banana-Dash
GADE7321 POE
## Overview
This project is a 3D racing game developed using Unity. It demonstrates my skills in game development while focusing heavily on the application of data structures and algorithms. The game features real-time racing with physics-based vehicle controls and incorporates advanced data structures for game mechanics, AI decision-making, and pathfinding.
## Key Data Structures and Design Patterns Used

1. **Queue ADT** – *Dialogue System*  
   Implemented to handle dialogues between characters. Each dialogue is enqueued, and the system processes and displays them in order.

2. **Stack ADT** – *Checkpoint Management*  
   Used to store player checkpoint progress. The stack tracks the last checkpoint reached, allowing for easy race reset functionality.

3. **Linked List ADT** – *Waypoint Navigation for AI*  
   AI racers use a linked list to store a sequence of waypoints on the race track. This allows the AI to target specific waypoints in the optimal order during the race.

4. **Factory Pattern** – *AI Racer Creation*  
   A **Factory Pattern** is implemented to create different types of AI racers. This pattern allows for easy expansion and the introduction of new AI behavior types without modifying existing code.

5. **State Design Pattern** – *Spectator Animation System*  
   The **State Design Pattern** is used to handle the different animation states for spectators. This allows spectators to smoothly transition between animations (e.g., cheering, clapping, or reacting to events on the track).

6. **Graph ADT** – *Advanced Race Pathfinding*  
   For more complex races, the **Graph ADT** is used to represent the track's waypoints. The AI can then randomly select a path to follow, adding more variety and challenge to the race dynamics.

7. **HashMap ADT** – *Sound Management*  
   A **HashMap** is used to store and quickly access sound effects and background music. Each key in the hashmap corresponds to a unique identifier for a sound, enabling fast retrieval during the race.
