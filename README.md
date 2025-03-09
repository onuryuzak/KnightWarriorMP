# Knight Warrior Multiplayer

This project is a multiplayer action game developed using Unity and Netcode for GameObjects. The game features a network-connected arena battle system where various characters can be used.

## Project Structure

The project is organized in a modular structure and follows clean code architecture principles. Here's the basic structure of the project:

### Main Directories

```
KnightWarriorMP/
├── Assets/
│   ├── 3rdParties/        # Third-party plugins and assets
│   ├── AddressableAssetsData/  # Addressable asset system configuration
│   ├── MP_Project/        # Main project assets and code
│   ├── Plugins/           # Plugins
│   ├── Resources/         # Directly loadable resources
│   ├── Scenes/            # Game scenes
│   ├── Settings/          # Project settings
│   └── TextMesh Pro/      # TextMesh Pro assets
├── Packages/              # Unity packages and dependencies
└── ProjectSettings/       # Unity project settings
```

### Code Structure

The code is organized in the `Assets/MP_Project/Scripts` directory:

```
Scripts/
├── Core/                  # Core systems
│   └── Network/           # Network infrastructure
│       ├── Lobby/         # Lobby system
│       ├── Managers/      # Network managers
│       ├── Network/       # Network components
│       └── Services/      # Network services
├── Managers/              # Game managers
├── Player/                # Player systems
│   ├── Controller/        # Player controls
│   ├── Entities/          # Player entities
│   ├── Factory/           # Player factory
│   ├── Interfaces/        # Player interfaces
│   └── Managers/          # Player managers
├── UI/                    # User interface
└── Utilities/             # Helper code components
```

### Game Scenes

The project includes the following scenes:

1. **Auth** - Authentication and login screen
2. **Lobby** - Lobby and room creation/joining screen
3. **Game** - Main game scene

## Architectural Structure

The project uses a component-based architecture. Here are the main components:

### Network Architecture

- **Unity Netcode for GameObjects** - Provides the multiplayer network infrastructure for the game
- **Unity Services Lobby** - Used for lobby and matchmaking systems

### Game Management

- **GameManager** - Manages the overall state of the game and controls player creation
- **LobbyManager** - Handles lobby operations and room management
- **AuthenticationManager** - Performs authentication and player identity management

### Player System

- **PlayerFactory** - Enables the creation of player objects
- **PlayerManager** - Manages player states and operations
- **PlayerController** - Manages player input and character control

## Dependencies

The project uses the following Unity packages:

- Unity Netcode for GameObjects (1.9.1)
- Unity Services Lobby (1.2.1)
- Unity Addressables (1.21.21)
- Unity Multiplayer Samples Coop
- Unity Universal Render Pipeline
- ParrelSync (For parallel testing during development)

## Setup and Development

1. Open the project through Unity Hub
2. It is recommended to use Unity 2023.3 or a newer version
3. Wait for the required packages to install
4. Open the Auth scene to start development

## Multiplayer Testing Process

There are two methods for multiplayer testing:

1. **ParrelSync** - Allows you to open multiple clients from a single Unity instance
2. **Build and Test** - Creating a build and testing with multiple clients

## Code Structure and Design Patterns

The project uses the following design patterns:

1. **Factory Pattern** - For creating player objects
2. **Manager Pattern** - For managing game systems
3. **Component Pattern** - Using Unity's component-based architecture
4. **Repository Pattern** - For data management

## Network Synchronization

The project uses various network synchronization approaches:

1. **NetworkVariables** - For state synchronization
2. **RPC Calls** - For client-server communication
3. **ClientRpc / ServerRpc** - For command communication between client and server
4. **Network Animator** - For animation synchronization

This README file provides a starting point for understanding the overall structure and architecture of the project. For more detailed information, you can examine the code files. 
