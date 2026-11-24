# Corrección, Trabajo Práctico N°07, Side Scroller 2D

- **Alumno/a:** Perez Cortez (Florencia Perez Cortez)
- **Entrega:** `TP07_C1_PerezCortez_Naruto-ShinobiAttack`
- **Materia:** Programación con Motores de Videojuegos I
- **Nota TP07:** 5

## 1. Checklist de requisitos

| #   | Requisito | Estado | Evidencia |
|-----|-----------|--------|-----------|
| R1  | Repositorio nuevo con nombre `TP07_C1_Apellido_Titulo` | ✓ | La carpeta y el remoto de `.git/config` son `TP07_C1_PerezCortez_Naruto-ShinobiAttack` |
| R2  | Proyecto de Unity con el mismo nombre | ✓ | `ProjectSettings/ProjectSettings.asset:16` `productName: TP07_C1_PerezCortez_Naruto-ShinobiAttack` |
| R3  | El personaje va a derecha e izquierda | ✓ | `Assets/_Project/Scripts/Player/PlayerController.cs:77` (`Movement()` en `FixedUpdate`), `Flip()` en la línea 61 |
| R4  | El personaje puede disparar | ✓ | `Assets/_Project/Scripts/Combat/PlayerCombat.cs:34` y `Shoot()`, `Projectile.Launch` en `Assets/_Project/Scripts/Combat/Projectile.cs:37` |
| R5  | El personaje puede saltar | ✓ | `Assets/_Project/Scripts/Player/PlayerController.cs:43` (ground check) y 49 (salto) |
| R6  | Enemigos u obstáculos que quitan vida | ✓ | `Assets/_Project/Scripts/Enemies/EnemyDamage.cs:7`, `Assets/_Project/Scripts/Enemies/RollingLog.cs:42`, `PlayerHealth.TakeDamage` |
| R7  | Se eliminan con más de 2 disparos, con vida y barra arriba (Canvas World) | ✓ | `Enemy.prefab` con `maxHealth: 3` y `Kunai_Data.asset` con `damage: 1` (3 impactos); el Canvas de `Enemy.prefab` está en World Space; `Assets/_Project/Scripts/UI/HealthBar.cs:9` |
| R8  | ParticleSystem al destruir al enemigo | ✓ | `Assets/_Project/Scripts/Enemies/EnemyHealth.cs:40` instancia `DeathEffect.prefab`, que tiene un `ParticleSystem` |
| R9  | Hay Pickables | ✓ | `Assets/_Project/Scripts/Pickables/Pickable.cs`, prefabs `Life`, `Ramen`, `Chakra` en `Assets/_Project/Prefabs/Pickables/` |
| R10 | Sonido en todas las acciones | ✓ | El `AudioManager` de `Gameplay.unity` tiene asignados `music`, `jumpClip`, `shootClip`, `enemyDeathClip`, `pickupLifeClip`, `pickupRamenClip` y `gameOverClip`. Se llaman desde `PlayerController.cs`, `PlayerCombat.cs`, `EnemyHealth.cs`, `Pickable.cs` y `GameManager.cs` |
| R11 | Volumen configurable desde Settings conectado a un AudioMixer | ✓ | `Assets/_Project/Scripts/Audio/AudioSettingsController.cs` (`audioMixer.SetFloat`), parámetros expuestos en `ShinobiAudioMixer.mixer:70`, AudioSources con su grupo del mixer asignado en las dos escenas |
| R12 | Usa ScriptableObject para datos o configuraciones | ✓ | `Assets/_Project/Scripts/ScriptableObjects/WeaponData.cs`, `Kunai_Data.asset`, `Rasengan_Data.asset` |
| R13 | Power ups o modificación de jugabilidad | ✓ | `Assets/_Project/Scripts/Player/PlayerPowerUp.cs:30` (`Activate` cambia el arma, el controller y da invulnerabilidad por `duration`) |
| R14 | Personaje animado | ✓ | `Assets/_Project/Animations/Player/Player.controller` (Idle, Run, Jump, Fall, Attack, Hurt, Death), `PlayerAnimation.cs` |
| R15 | Escena de menú principal y otra de Gameplay | ✓ | `ProjectSettings/EditorBuildSettings.asset`: `MainMenu.unity` y `Gameplay.unity` |

Sobre R15: en `MainMenu.unity` el campo `gameSceneName` dice `GamePlay` y el archivo es `Gameplay.unity`. La escena carga bien en el build de itch.io, así que el requisito está cumplido, pero usá el nombre exacto de la escena (`Gameplay`). Lo dejé como Warning.

## 2. Cumplimiento de Pautas de Cursada

| ID | Pauta | Estado | Evidencia |
|----|-------|--------|-----------|
| P1 | Unity 6000.3.21f1 | ✗ | `ProjectSettings/ProjectVersion.txt` dice `6000.3.11f1` |
| P2 | Repo de GitHub público | ✓ | El repositorio es público |
| P3 | Nombre del repo | ✓ | `TP07_C1_PerezCortez_Naruto-ShinobiAttack` respeta `TPxx_Cn_Apellido_Titulo` |
| P4 | Código en inglés | ✓ | En los 26 scripts de `Assets/_Project` las clases, métodos, campos y variables están en inglés. Los textos de `[Header]` ("Fuentes", "Musica", "Efectos") y los comentarios están en español, y eso está permitido |
| P5 | Un repo por TP, sin branches | ✓ | Solo existe `main` |

## 3. Estructura de carpetas

Tenés todo dentro de `Assets/_Project/`, así que revisé la estructura dentro de esa carpeta.

| Carpeta esperada | Encontrada | Estado |
|------------------|------------|--------|
| `Scenes/` | `Assets/_Project/Scenes/` | ✓ |
| `Scripts/` | `Assets/_Project/Scripts/` (con subcarpetas Audio, Combat, Enemies, Enviroment, Managers, Pickables, Player, ScriptableObjects, UI) | ✓ |
| `Prefabs/` | `Assets/_Project/Prefabs/` | ✓ |
| `Art/Sprites/` | No existe. Los sprites están en `Art/shinobi_attack_sprites/`, `Art/Zetsu/` y `Art/Ground.png` | ✗ |
| `Art/Animations/` | Están en `Assets/_Project/Animations/` | Ubicación alternativa, observación |
| `Art/Audio/` | Están en `Assets/_Project/Audio/` | Ubicación alternativa, observación |
| `Art/Fonts/` | La fuente está en `Art/UI/njnaruto.ttf` | Ver archivos mal ubicados |
| `Data/` | No existe. Los `.asset` de ScriptableObject están en `Assets/_Project/ScriptableObjects/Weapon/` | ✗ |

Archivos mal ubicados:

- `Assets/_Project/MainMenu/Scripts/MainMenuController.cs` es un script fuera de `Scripts/`, va en `Assets/_Project/Scripts/UI/`.
- `Assets/_Project/Art/UI/njnaruto.ttf` y `njnaruto SDF.asset` son la fuente y están fuera de `Art/Fonts/`.

Otras cosas: la carpeta `Enviroment` tiene un error de tipeo (es `Environment`). `InputSystem_Actions.inputactions` está en `Assets/_Project/Settings/`.

## 4. Qué le falta

- Agregá un flag de muerte en `PlayerHealth` y en `EnemyHealth` para que `Die()` no se ejecute dos veces.
- Bloqueá el disparo y el salto cuando `Time.timeScale` es 0.
- Usá dos contenedores distintos para enemigos y troncos en `Gameplay.unity`.
- Encapsulá los campos públicos de `WeaponData`.
- Reemplazá los strings mágicos (parámetros del Animator, claves de `PlayerPrefs`, nombres de escena) por `const` o hashes.
- Reemplazá `GameObject.Find` y `FindWithTag` en cada instancia por referencias que le pase el spawner.
- Actualizá a Unity 6000.3.21f1.

## 5. Hallazgos de código

### Error (0)

### Bug (4)
- `Assets/_Project/Scripts/Combat/PlayerCombat.cs:32` Con `Time.timeScale = 0` se puede seguir disparando desde `Update`, con el menú de pausa abierto.
- `Assets/_Project/Scripts/Player/PlayerHealth.cs:35` Como no hay flag de muerto, `Die()` se puede ejecutar de nuevo pasado `invulnerabilityTime`, y `Heal()` recarga corazones estando muerto.
- `Assets/_Project/Scripts/Enemies/EnemyHealth.cs:17` Dos impactos en el mismo frame ejecutan `Die()` dos veces: doble puntaje, doble drop y doble partícula.
- `Assets/_Project/Scripts/Enemies/EnemySpawner.cs:46` En `Gameplay.unity`, `enemiesParent` y `rollingLogsParent` son el mismo Transform, así que los troncos cuentan para `maxEnemies`.

### Warning (12)
- `Assets/_Project/MainMenu/Scripts/MainMenuController.cs:7` `gameSceneName` vale `GamePlay` y la escena es `Gameplay.unity`.
- `Assets/_Project/MainMenu/Scripts/MainMenuController.cs:37` `CloseOptions()` en `Start` reproduce un click al cargar el menú.
- `Assets/_Project/Scripts/Combat/PlayerCombat.cs:71` Parámetros del Animator como strings mágicos.
- `Assets/_Project/Scripts/Player/PlayerController.cs:47` Modificás la velocidad del Rigidbody2D en `Update` para el salto.
- `Assets/_Project/Scripts/Player/PlayerAnimation.cs:18` Tres strings de parámetros del Animator en cada frame.
- `Assets/_Project/Scripts/Enemies/EnemyDamage.cs:9` `GetComponent` en cada paso de `OnCollisionStay2D`.
- `Assets/_Project/Scripts/Enemies/EnemyDrop.cs:15` `GameObject.Find("Pickables")` por cada enemigo, y ese objeto no existe en `Gameplay.unity`.
- `Assets/_Project/Scripts/Enemies/EnemyMovement.cs:22` `FindWithTag("Player")` en cada enemigo instanciado.
- `Assets/_Project/Scripts/Managers/GameManager.cs:52` Nombre de escena `"MainMenu"` hardcodeado.
- `Assets/_Project/Scripts/UI/PauseMenuController.cs:18` Agregás listeners en `OnEnable` y no hay `OnDisable`.
- `Assets/_Project/Scripts/ScriptableObjects/WeaponData.cs:6` Campos `public` mutables en el ScriptableObject.
- `Assets/_Project/Scripts/Pickables/Pickable.cs:42` `GetComponent<PlayerPowerUp>()` sin null check.

### Suggestion (11)
- `Assets/_Project/MainMenu/Scripts/MainMenuController.cs:43` El click se reproduce después de `LoadScene` y de `Application.Quit`.
- `Assets/_Project/Scripts/Combat/PlayerCombat.cs:3` `using Unity.VisualScripting.Antlr3.Runtime` sin uso.
- `Assets/_Project/Scripts/Combat/Projectile.cs:9` `enemy` debería ser una variable local.
- `Assets/_Project/Scripts/Player/PlayerController.cs:19` `Move` y `Jump` están en PascalCase, distinto del resto.
- `Assets/_Project/Scripts/Player/PlayerHealth.cs:77` `GetComponent` en `Die()`, conviene cachearlo en `Awake`.
- `Assets/_Project/Scripts/Player/PlayerPowerUp.cs:45` `playerHealth` sin validar.
- `Assets/_Project/Scripts/Enemies/EnemyMovement.cs:54` Número mágico `-20f`, repetido en `RollingLog`.
- `Assets/_Project/Scripts/Enemies/EnemySpawner.cs:87` Variable local `distance` sin uso.
- `Assets/_Project/Scripts/Managers/ScoreManager.cs:9` `recordtext` no respeta camelCase.
- `Assets/_Project/Scripts/Managers/ScoreManager.cs:30` La clave `"Record"` se repite sin `const`.
- `Assets/_Project/Scripts/Audio/AudioSettingsController.cs:32` Nombres de parámetros del mixer repetidos sin `const`.

`Assets/TextMesh Pro/` es un paquete importado y no lo revisé. Los `Debug.Log` y `Debug.LogError` de `MainMenuController.Quit` y de las validaciones de `PlayerCombat.Shoot` son esporádicos, no los marco.

## 6. Recomendaciones

1. Agregá `isDead` en `PlayerHealth` y `EnemyHealth`, y cortá el input de disparo y salto cuando el juego está pausado. Son los tres bugs que más se notan jugando.
2. Separá `enemiesParent` y `rollingLogsParent` en dos objetos distintos de `Gameplay.unity`.
3. Pasá a `const` o `Animator.StringToHash` los strings de Animator, `PlayerPrefs`, mixer y escenas, y corregí `gameSceneName` a `Gameplay`.
4. Encapsulá `WeaponData` con `[SerializeField] private` y properties de solo lectura.
5. Agregá `OnDisable` en `PauseMenuController` y reemplazá `Find`/`FindWithTag` por referencias que le pase `EnemySpawner`.
6. Reorganizá `Art/Sprites/`, `Art/Fonts/`, `Data/` y mové `MainMenuController.cs` a `Scripts/UI/`.
7. Actualizá el proyecto a Unity 6000.3.21f1.
