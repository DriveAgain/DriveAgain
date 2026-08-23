# 🚗 DriveAgain

### A Serious Game for Cognitive and Motor Driving Rehabilitation

**DriveAgain** is a rehabilitation-oriented driving game designed to support the gradual practice of driving-related cognitive and motor skills following traumatic brain injury (TBI).

The game combines driving simulation, progressive difficulty, immediate feedback, and a reward-based progression system to create an engaging environment in which players can practice important driving skills in a controlled setting.

The project was developed as an interdisciplinary collaboration between **Computer Science** and **Occupational Therapy** students.

> **DriveAgain — Regain control. Rebuild confidence. Drive again.**

---

## 🎯 Project Goals

Returning to driving after a traumatic brain injury can require the rehabilitation of multiple cognitive and motor abilities.

DriveAgain was designed to provide a controlled environment for practicing:

* 🚗 Vehicle control and coordination
* ⚡ Reaction time
* 🧠 Sustained and divided attention
* 👀 Visual information processing
* 🛣️ Following traffic rules and road signs
* 🎯 Planning and continuous monitoring
* ⏱️ Performing tasks under time constraints
* 🧩 Decision-making in a dynamic environment

The game is **not intended to replace professional driving rehabilitation**. Instead, it is designed as a complementary training and practice tool.

---

# 🎮 Gameplay

The player controls a vehicle using the keyboard and navigates through progressively more challenging driving environments.

During gameplay, the player must:

* Control the vehicle and maintain appropriate speed
* Follow the road and navigate turns
* React to different driving situations
* Avoid mistakes and obstacles
* Follow traffic-related instructions
* Complete the level within the required conditions

The game provides immediate feedback and rewards the player according to their performance.

---

# 🏁 Progressive Difficulty

DriveAgain currently contains several levels with increasing difficulty.

### ⭐ Tutorial

The tutorial introduces the player to the basic game mechanics.

* Learn vehicle controls
* Practice basic driving
* Become familiar with the game environment
* Introduce the game's rules and objectives
* Required before unlocking the other levels

### ⭐ Easy

Focuses on basic vehicle control and continuous attention.

* Simple route
* Basic turns
* Lower cognitive load
* Practice maintaining control of the vehicle

### ⭐⭐ Medium

Introduces additional challenges and requires the player to manage several elements simultaneously.

* More complex route
* Increased demand for attention
* Faster reactions
* Multiple skills practiced at the same time

### ⭐⭐⭐ Hard

Designed to provide the highest level of challenge currently available.

* More demanding route
* Increased speed and pressure
* Higher attentional requirements
* Greater precision and reaction demands

The progressive structure allows players to gradually increase the difficulty rather than being exposed to all challenges at once.

---

# ⏱️ Performance & Reward System

DriveAgain uses a performance-based reward system to encourage accuracy, consistency, and efficient completion of levels.

### ⭐ Stars

Players can earn up to **3 stars** according to their performance.

Mistakes and poor performance can reduce the player's score.

### 💰 Money

Players earn in-game currency through successful gameplay.

The currency is used to unlock additional content in the game's shop.

### 🏆 Time Bonus

Some challenges reward the player for completing a level quickly while maintaining successful performance.

For example:

> **Complete the level in under 1:30 and receive a 100-point bonus.**

This creates an additional challenge that encourages players to improve both their speed and accuracy.

---

# 🚘 Vehicle & Shop System

DriveAgain includes a simple in-game shop that allows players to use their earned currency to unlock different vehicles.

Available vehicles include:

| Vehicle          | Cost |
| ---------------- | ---: |
| Default Gray Car | Free |
| Blue Car         |  300 |
| Red Car          |  600 |
| Yellow Car       |  900 |

The shop provides an additional progression mechanism and gives players a visual reward for continued gameplay.

---

# 🗺️ Navigation & HUD

The game includes several UI systems designed to provide the player with relevant information during gameplay.

### In-Game HUD

The HUD currently displays:

* 🚗 Current vehicle speed
* ⭐ Current stars
* 💰 Available money
* ⏱️ Current level time
* 🗺️ Mini-map

The game also includes environmental boundaries and invisible walls to keep the player within the intended driving area.

---

# 💾 Progression & Cloud Saving

Player progression is connected to **Unity Cloud Save**.

The system is designed to store important progression information such as:

* Unlocked levels
* In-game currency
* Player progression

This allows progression to persist between game sessions when the appropriate account/session configuration is available.

---

# 🛠️ Technology Stack

### Game Engine

* **Unity**

### Programming

* **C#**

### UI

* **TextMeshPro**
* Unity UI

### Cloud Services

* **Unity Cloud Save**

### Version Control

* **Git**
* **GitHub**

### Platform

* WebGL

---

# 📂 Project Structure

```text
DriveAgain/
│
├── Assets/
│   ├── Scenes/
│   ├── Scripts/
│   ├── Prefabs/
│   ├── Materials/
│   └── ...
│
├── README.md
├── Research_Market.md
├── elements-formal.md
├── dynamic.md
└── planning-levels.md
```

### Main Scripts

| Script                  | Responsibility                            |
| ----------------------- | ----------------------------------------- |
| `CarMovement.cs`        | Vehicle movement and player controls      |
| `FinishLineLoader.cs`   | Detects level completion                  |
| `LevelSelectManager.cs` | Handles level unlocking and progression   |
| `SuccessUI.cs`          | Displays the successful-completion screen |
| `SuccessCloudSaver.cs`  | Handles saving successful progress        |
| `TimerHUD.cs`           | Tracks and displays gameplay time         |

---

# 🧪 Research & User Testing

DriveAgain was developed as an interdisciplinary project combining software development with occupational-therapy considerations.

The development process included user testing and feedback collection in order to evaluate the game's usability and the player's interaction with the training experience.

The feedback was used to improve aspects such as:

* Game instructions
* Difficulty progression
* User interface
* Feedback and rewards
* Overall gameplay experience

A questionnaire was also developed to examine relevant driving knowledge and the player's experience with the game.

---

# 🧠 Rehabilitation Approach

The design of DriveAgain is based on the idea that rehabilitation can benefit from repeated practice in an engaging and controlled environment.

Rather than presenting the player with a traditional questionnaire or exercise alone, the game allows the player to **practice driving-related skills through interaction**.

The combination of:

**Practice → Feedback → Reward → Progression → Increased Difficulty**

is intended to encourage repeated engagement and gradual improvement.

---

# 🎓 Academic Context

DriveAgain was developed as part of a **Computer Game Development** course through an interdisciplinary collaboration between:

* **Department of Computer Science**
* **Department of Occupational Therapy**

The project combines software engineering, game development, user experience, and rehabilitation-oriented design.

---

# 👥 Development Team

### Computer Science

**Emuna Fonda Sofer**
Computer Science
Student ID: 213204837

### Occupational Therapy

**Tamima Fruchtman**
Student ID: 328610720

**Tehila Partush**
Student ID: 304893266

**Goldie Radko**
Student ID: 323812107

---

# 🎮 Play the Game

**WebGL Build:**
https://emunasofer.itch.io/drive-again

# 💻 Repository

**GitHub:**
https://github.com/DriveAgain/DriveAgain

---

# 🚀 Future Development

Potential future development includes:

* Additional driving scenarios
* More complex traffic situations
* Additional cognitive challenges
* More detailed performance analysis
* Expanded customization options
* Additional rehabilitation-oriented exercises
* Improved progress tracking

---

## ✨ DriveAgain

**Regain control. Rebuild confidence. Drive again.**

*An interactive rehabilitation experience designed to make practice engaging, measurable, and progressive.*
