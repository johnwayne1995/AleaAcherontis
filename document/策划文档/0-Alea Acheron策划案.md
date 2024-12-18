# Alea Acheron玩法策划案

| 作者          | 时间     | 版本 | 说明     |
| ------------- | -------- | ---- | -------- |
| changlin.tian | 20241025 | 1.0  | 创建文档 |
|               |          |      |          |





## 一、概述

### 1.1 游戏介绍

- **类型：**牌组构建式肉鸽
- **平台：**PC
- **玩法：**游戏以单人肉鸽闯关为核心目的，玩家需要在对局中通过牌组构筑，获得合适的协同效果，打出手牌并造成伤害，来打败每关的怪物。

### 1.2 游戏乐趣

- 卡牌版战斗爽游，享受数字爆炸的快感

- 牌组构筑类rouge的策略乐趣

### 1.3 故事背景

![image-20241025172523695](https://github.com/johnwayne1995/AleaAcherontis/blob/main/document/documentImg/image-20241025172523695.png?raw=true)



## 二、游戏控制

游戏中主要通过鼠标左键进行控制。

### 2.1 移动和方向控制

- **点击地面移动**：玩家点击地面上的某个位置，角色会自动移动到该位置。

- **点击障碍物**：若玩家点击的是障碍物，角色会绕过障碍物移动到指定位置。

### 2.2 物体交互

- **NPC 交互**：玩家点击 NPC，角色会自动移动到 NPC 旁边并触发交互对话或事件。
- **物品交互和拾取**：玩家点击房间内放置的物品/宝箱，角色会自动移动到物品旁并拾取。
- **开关门**：玩家点击门或开关，角色会自动移动到门或开关旁并触发相应的开关操作。

### 2.3 战斗（出牌）

- **选中/取消选中卡牌：**点击对应卡牌可选中，再次点击即可取消。
- 其余交互逻辑见【四、战斗】 



## 三、地图与关卡

### 3.1 关卡

#### 3.1.1 整体关卡结构

游戏中的关卡一共分为X层，采用线性关卡的形式。每层固定结构为：2（普通怪物房）+1（随机房间）+1个房间。

#### 3.1.2 游戏通关规则

玩家完成所有层数的首领房挑战，即可通关。

游戏通关后会进入无尽模式，详见【六、无尽模式】。



### 3.2 关卡类型

#### 3.2.1 普通怪物房

**（1）基础规则**

- **难度：**挑战压力较小，可获得金币奖励。

- **跳过关卡：**从第二层开始，玩家可选择跳过怪物房。



**（2）怪物对话（注：涉及是否跳过此关）**

- **打招呼对话：**玩家进入普通怪物房后，根据每个关卡的不同，对应怪物会触发一句打招呼对话。对话框播放X秒后，自动消失。

  ![image-20241025192600959](https://github.com/johnwayne1995/AleaAcherontis/blob/main/document/documentImg/image-20241025192600959.png?raw=true)

- **邀请战斗对话：**

  - **触发规则：**玩家点击怪物，怪物会播放战斗邀请对话。
  - **战斗选项**: 玩家可以选择“战斗”，此时房间内所有的门立即自动关闭，怪物出现并开始战斗。
  - **放弃选项（跳过关卡）**: 玩家可以选择“放弃”的选项，此时玩家可以自行离开房间，不与怪物战斗。

![image-20241025191150077](https://github.com/johnwayne1995/AleaAcherontis/blob/main/document/documentImg/image-20241025191150077.png?raw=true)

- **触发次数限制：**当玩家已经进行过对话选择，则无法再次通过同一怪物对话触发战斗。若玩家再次和怪物交互，会播放怪物拒绝战斗的对话。

![image-20241025191825477](https://github.com/johnwayne1995/AleaAcherontis/blob/main/document/documentImg/image-20241025191825477.png?raw=true)



- **数据配置**：选项是否解锁，即关卡是否可跳过，配置在关卡表中。



#### 3.2.2 NPC房（随机房间）

##### （1）定义

关卡中唯一不固定内容的房间类型。玩家在进入此类房间时，会随机遇到一种不同的事件或挑战。



##### （2）类型

可能遭遇的NPC类型暂定如下：

1. 许愿池：花费指定金币，三选一一个效果。花费的数额会越来越高

2. 商人，会有三个物品刷新在商店，消耗金币可刷新一次

3. 宝箱，白送一个物品，有15%概率被宝箱怪咬一口，扣除1%百分比血量

4. 祝福女神，三选一一个只能在本层使用的祝福



*（第一阶段demo只做宝箱一种即可）*



#### 3.2.3 BOSS房

##### **（1）进入规则**

- **自动锁门**: 玩家一旦进入首领房，房间内所有的门立即自动关闭，无法逃离。
- **触发战斗**: 进入房间后，首领怪物立即出现并开始战斗。

##### **（2）战斗胜利**

- **解锁门**: 玩家击败首领怪物后，除了上一个房间的门是关闭的，无法再进入外，其他朝向的门（若有，如北、东、南、西）都会自动打开，玩家可继续探索。





## 四、战斗（卡牌）

### 4.1 基础规则

- 玩家每回合可以选择出牌或弃牌。
- 每次出牌都会对敌人造成伤害，伤害值基于出牌的类型和等级。
- 玩家可以通过战斗和任务获得新的卡牌，并且可以升级和管理自己的卡组。

### 4.2 卡牌类型

#### 4.2.1 扑克牌

![image-20241025171725368](https://github.com/johnwayne1995/AleaAcherontis/blob/main/document/documentImg/image-20241025171725368.png?raw=true)

玩家的手牌，每次打出后都可对敌人造成伤害。基于德州扑克的顺子、对子等计算基础伤害。

- **升级：**可以升级品级，以获得更高的基础伤害。白<紫<金
- **丢弃或销毁：**可以被丢弃或销毁

#### 4.2.2 魔神牌

![image-20241025171830871](https://github.com/johnwayne1995/AleaAcherontis/blob/main/document/documentImg/image-20241025171830871.png?raw=true)

仅可通过战胜房间怪物获得，用于提升不同牌组的伤害倍率
不可升级，可以丢弃或销毁



#### 4.2.3 道具牌

![image-20241025171839580](https://github.com/johnwayne1995/AleaAcherontis/blob/main/document/documentImg/image-20241025171839580.png?raw=true)

**（1）类型**（暂定，有余力后续再扩展）
**升级伤害：**用于升级对子、顺子等牌型的等级，每一级会提升对应牌型的基础伤害。
**削减血量：**装备后可让进场的怪物血量削减一定百分比（进场判定，每一关卡仅触发一次）



**（2）使用时效**

部分道具牌可能会根据不同情况，进行自动销毁。分为以下几种：

1. 在使用一定次数后必定销毁；
2. 使用后有X%概率销毁；
3. 可以在一整轮游戏中使用，不会销毁；





### 4.4 战斗操作

#### 4.4.1 出牌

- 玩家每回合可以选择一张或多张手牌打出。
- 根据打出的牌型（如顺子、对子等），计算并对敌人造成相应的伤害。
- 出牌后，该牌将从玩家的手牌中移除。

#### 4.4.2 弃牌

- 玩家可以选择弃掉不需要的手牌。
- 弃牌后，该牌将从玩家的手牌中移除，进入弃牌堆。
- 弃牌不会对敌人造成任何伤害，但可以为玩家腾出手牌空间，以便抽取新的牌。

#### 4.4.3 查看牌型

- 玩家可以随时查看当前手牌的牌型。
- 系统会自动识别并提示玩家当前手牌中可能组成的最佳牌型（如顺子、对子等）。
- 查看牌型功能可以帮助玩家制定出牌策略，最大化伤害输出。
- 点击“查看牌型”按钮，会加载“牌型一览”的UI界面。



### 4.5 伤害计算

#### 4.5.1 伤害计算方式

- **基础伤害：** 根据打出的牌型确定基础伤害值。例如，顺子、对子等不同牌型有不同的基础伤害值。
- **卡牌品级加成** 高品级的牌（如紫、金）会有额外的伤害加成。
- **魔神牌加成：** 如果玩家拥有魔神牌，打出的牌型将获得魔神牌提供的伤害倍率加成。
- **道具牌加成**： 部分道具牌可以增加特定牌型的伤害，使用这些道具牌后，计算伤害时将考虑这些加成。

#### 4.5.2 血量扣除

- **敌人血量：** 敌人的初始血量根据关卡和怪物类型而定。
- **血量扣除：** 每次玩家打出牌型并造成伤害后，敌人的血量将减少相应的伤害值。
- **道具效果：** 如果玩家装备了削减血量的道具牌，敌人的血量会在进场时按比例减少。
- **战斗结束：** 当敌人的血量降至 0 或以下时，战斗结束，玩家获胜。若玩家的血量降至 0 或以下，则战斗失败，输出Game over画面。

## 六、无尽模式（第一版demo不用做）

- 无尽模式下，会在每次进入房间时，遇到之前遇到的首领怪物，战斗机制复用，仅血量等数值提升。
