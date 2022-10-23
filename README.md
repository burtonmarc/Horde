# Horde

This project aims to replicate the game Survivor.io, with its Main Menu, Gameplay, and Progression system.

The goal is to add the following main systems, which some of them are already done or WIP:

- Addressables                                    | DONE
- Screen Machine                                  | DONE
- Catalogs                                        | DONE
- Persistance (using Playfab)                     | TODO
- Controller/View Factory                         | DONE
- Pooling system                                  | WIP
- Gameplay Entities Container                     | WIP
- Waves System                                    | WIP

The Screen Machine allows for control over which screens (or states) to present and what happens to the ones that stay behind. The Addressables 
system implemented allows to load the WorldView, UiView and State-specific configs before the new state is open. These views and configs are stored
in catalogs, specifically the states catalog. The Screen Machine also allows to preload any shared addressables that might be of interest to a 
specific state. For example, any weapon, which are not state-specific, can be preloaded by any state.

Right now the game consists of mainly Gameplay, where the main work has been done. The ControllerView factory allows for easy creation of Entities 
which have both logic (GameplayController), and a view (GameplayView), and works together with the Pooling system to reuse Controllers and Views. 

The Pooling system right now reuses each Controller and it's View, and when necessary it re-initializes both with a new model and config. In the
future I want it to keep the pooled entities in the original list so it doesn't move the elements and resizes the array. Right now it moves them to 
a new list.

The entities container manages all the entities' update and collision order. This way I can update the movement and calculate the colisions of an entity
in the same iteration. I still have to check the performance of this method and wether to move to ECS.
