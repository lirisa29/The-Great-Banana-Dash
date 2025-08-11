# The-Great-Banana-Dash
GADE7321 POE
## Overview
The Great Banana Dash is a 3D racing game developed entirely by me using Unity. This project demonstrates my skills in game development with a particular focus on applying data structures and algorithms to enhance gameplay, AI behaviour, and game mechanics.

Beyond programming, I designed and built all the maps and tracks, modeled and textured them using Blender with vector colouring and Shader Graph techniques. Most props were sourced from the Unity Asset Store, but the overall level design and environment composition are my original work.

I created custom lighting setups, post-processing effects, and skyboxes crafted in Photoshop to establish a unique visual atmosphere. All UI elements were designed from scratch using Photoshop alongside AI-assisted tools, contributing to a polished player experience.

Additionally, I sourced all background music and sound effects from Pixabay, integrating them carefully to complement the game’s pacing and feel.
## Key Design Decisions and Features
**Game Concept and Theme**
I chose to develop The Great Banana Dash as a vibrant 3D kart racing game set in a lush jungle environment. This concept allowed me to combine a fun, approachable theme with the technical challenge of implementing complex data structures and gameplay mechanics.

The jungle and monkey theme gave me creative freedom to design colourful, dynamic tracks that stand out visually and offer varied racing experiences. It also allowed for playful mechanics like banana collection and drifting boosts, which add depth and player engagement without overwhelming complexity.

**Custom Gameplay Mechanics**
- **Drifting Mechanic with Speed Boost**:
I implemented a drifting system that grants the player a temporary speed boost proportional to the drift duration, adding a rewarding skill-based element to gameplay.
- **Respawning System**:
Developed a custom respawn feature to allow players to reset to the last checkpoint seamlessly without interrupting race flow.
- **Minimap System**:
Beyond project requirements, I created a minimap using Unity’s render textures, which involved learning new rendering techniques and enhancing player navigation awareness.
## Technical Highlights
**Data Structures & Design Patterns Used**
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
## What I Learned
- How to effectively integrate various data structures and design patterns in a real-time game environment to optimise AI behaviour and player experience.
- Practical experience with Blender’s vector colouring and shader techniques to create visually appealing and performant track assets.
- The workflow of designing and implementing custom lighting and post-processing effects to enhance atmosphere and mood in Unity.
- Using render textures in Unity to build functional UI elements like a minimap, expanding my knowledge of advanced rendering techniques.
- Balancing gameplay mechanics such as drifting speed boosts and respawning to maintain player engagement and fairness.
## Future Improvements & Challenges
While developing The Great Banana Dash, I encountered several technical and design challenges that expanded my problem-solving skills. For example, implementing the drifting mechanic and ensuring smooth respawning required careful tuning of physics and player controls to balance fun and fairness.

In future versions, I’d like to improve the AI behaviour by making racers adapt dynamically to player actions, enhancing race variety and competitiveness. I also plan to add a leaderboard and multiplayer functionality, which would greatly increase replayability.
