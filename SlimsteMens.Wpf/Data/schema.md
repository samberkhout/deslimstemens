# Game JSON Schema

- `Rounds`: array of round objects
  - `Name`: string
  - `Type`: `OpenDoor | Puzzle | Finale`
  - `Questions`: array of questions
    - `Text`: string
    - `Options`: array of answer options
      - `Text`: string
      - `IsCorrect`: boolean
- `Teams`: array of teams
  - `Name`: string
  - `Seconds`: integer
