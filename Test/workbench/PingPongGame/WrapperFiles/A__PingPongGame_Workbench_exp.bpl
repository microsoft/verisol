type Ref;
type ContractName;
const unique null: Ref;
const unique WorkbenchBase: ContractName;
const unique Starter_AzureBlockchainWorkBench: ContractName;
const unique Player_AzureBlockchainWorkBench: ContractName;
const unique Starter: ContractName;
const unique Player: ContractName;
function ConstantToRef(x: int) returns (ret: Ref);
function keccak256(x: int) returns (ret: int);
var DType: [Ref]ContractName;
var Alloc: [Ref]bool;
var balance_ADDR: [Ref]int;
var Length: [Ref]int;
procedure {:inline 1} FreshRefGenerator() returns (newRef: Ref);
implementation FreshRefGenerator() returns (newRef: Ref)
{
havoc newRef;
assume ((Alloc[newRef]) == (false));
Alloc[newRef] := true;
}

procedure {:inline 1} HavocAllocMany();
implementation HavocAllocMany()
{
var oldAlloc: [Ref]bool;
oldAlloc := Alloc;
havoc Alloc;
assume (forall  __i__0_0:Ref :: ((oldAlloc[__i__0_0]) ==> (Alloc[__i__0_0])));
}

procedure boogie_si_record_sol2Bpl_int(x: int);
procedure boogie_si_record_sol2Bpl_ref(x: Ref);
procedure boogie_si_record_sol2Bpl_bool(x: bool);

axiom(forall  __i__0_0:int, __i__0_1:int :: (((__i__0_0) == (__i__0_1)) || ((ConstantToRef(__i__0_0)) != (ConstantToRef(__i__0_1)))));

axiom(forall  __i__0_0:int, __i__0_1:int :: (((__i__0_0) == (__i__0_1)) || ((keccak256(__i__0_0)) != (keccak256(__i__0_1)))));
var ApplicationName_WorkbenchBase: [Ref]int;
var WorkflowName_WorkbenchBase: [Ref]int;
procedure {:inline 1} WorkbenchBase_WorkbenchBase_NoBaseCtor(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, applicationName_s40: int, workflowName_s40: int);
implementation WorkbenchBase_WorkbenchBase_NoBaseCtor(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, applicationName_s40: int, workflowName_s40: int)
{
// start of initialization
assume ((msgsender_MSG) != (null));
ApplicationName_WorkbenchBase[this] := 720258860;
WorkflowName_WorkbenchBase[this] := 720258860;
// end of initialization
ApplicationName_WorkbenchBase[this] := applicationName_s40;
call  {:cexpr "ApplicationName"} boogie_si_record_sol2Bpl_int(ApplicationName_WorkbenchBase[this]);
WorkflowName_WorkbenchBase[this] := workflowName_s40;
call  {:cexpr "WorkflowName"} boogie_si_record_sol2Bpl_int(WorkflowName_WorkbenchBase[this]);
}

procedure {:constructor} {:public} {:inline 1} WorkbenchBase_WorkbenchBase(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, applicationName_s40: int, workflowName_s40: int);
implementation WorkbenchBase_WorkbenchBase(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, applicationName_s40: int, workflowName_s40: int)
{
call WorkbenchBase_WorkbenchBase_NoBaseCtor(this, msgsender_MSG, msgvalue_MSG, applicationName_s40, workflowName_s40);
}

procedure {:public} {:inline 1} ContractCreated_WorkbenchBase(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int);
implementation ContractCreated_WorkbenchBase(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int)
{
}

procedure {:public} {:inline 1} ContractUpdated_WorkbenchBase(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, action_s65: int);
implementation ContractUpdated_WorkbenchBase(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, action_s65: int)
{
}

procedure {:inline 1} Starter_AzureBlockchainWorkBench_Starter_AzureBlockchainWorkBench_NoBaseCtor(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, gameName_s95: int);
implementation Starter_AzureBlockchainWorkBench_Starter_AzureBlockchainWorkBench_NoBaseCtor(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, gameName_s95: int)
{
// start of initialization
assume ((msgsender_MSG) != (null));
// end of initialization
assert ((State_Starter[this]) == (0));
call ContractCreated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG);
}

procedure {:constructor} {:public} {:inline 1} Starter_AzureBlockchainWorkBench_Starter_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, gameName_s95: int);
implementation Starter_AzureBlockchainWorkBench_Starter_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, gameName_s95: int)
{
call WorkbenchBase_WorkbenchBase_NoBaseCtor(this, msgsender_MSG, msgvalue_MSG, -962522124, -1638439881);
call Starter_Starter_NoBaseCtor(this, msgsender_MSG, msgvalue_MSG, gameName_s95);
call Starter_AzureBlockchainWorkBench_Starter_AzureBlockchainWorkBench_NoBaseCtor(this, msgsender_MSG, msgvalue_MSG, gameName_s95);
}

procedure {:public} {:inline 1} Transition_GameProvisioned_Number_0_StartPingPong_Starter_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongTimes_s126: int);
implementation Transition_GameProvisioned_Number_0_StartPingPong_Starter_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongTimes_s126: int)
{
assume ((State_Starter[this]) == (0));
assume ((msgsender_MSG) == (GameStarter_Starter[this]));
if ((DType[this]) == (Starter_AzureBlockchainWorkBench)) {
call StartPingPong_Starter_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, pingPongTimes_s126);
}
//assert ((State_Starter[this]) == (1) || (State_Starter[this]) == (2));
assert ((State_Starter[this]) == (2));
}

procedure {:public} {:inline 1} StartPingPong_Starter_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongTimes_s144: int);
implementation StartPingPong_Starter_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongTimes_s144: int)
{
call StartPingPong_Starter(this, msgsender_MSG, msgvalue_MSG, pingPongTimes_s144);
call ContractUpdated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, -499343974);
}

procedure {:public} {:inline 1} Pong_Starter_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, currentPingPongTimes_s162: int);
//ensures ((State_Starter[this]) == (1) || (State_Starter[this]) == (2));
ensures ((State_Starter[this]) == (2));
implementation Pong_Starter_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, currentPingPongTimes_s162: int)
{
call Pong_Starter(this, msgsender_MSG, msgvalue_MSG, currentPingPongTimes_s162);
call ContractUpdated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, 1280674614);
}

procedure {:public} {:inline 1} FinishGame_Starter_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int);
implementation FinishGame_Starter_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int)
{
call FinishGame_Starter(this, msgsender_MSG, msgvalue_MSG);
call ContractUpdated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, 18166401);
}

procedure {:inline 1} Player_AzureBlockchainWorkBench_Player_AzureBlockchainWorkBench_NoBaseCtor(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongGameName_s207: int);
implementation Player_AzureBlockchainWorkBench_Player_AzureBlockchainWorkBench_NoBaseCtor(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongGameName_s207: int)
{
// start of initialization
assume ((msgsender_MSG) != (null));
// end of initialization
assert ((State_Player[this]) == (0));
call ContractCreated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG);
}

procedure {:constructor} {:public} {:inline 1} Player_AzureBlockchainWorkBench_Player_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongGameName_s207: int);
implementation Player_AzureBlockchainWorkBench_Player_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongGameName_s207: int)
{
call WorkbenchBase_WorkbenchBase_NoBaseCtor(this, msgsender_MSG, msgvalue_MSG, -962522124, 1194047632);
call Player_Player_NoBaseCtor(this, msgsender_MSG, msgvalue_MSG, pingPongGameName_s207);
call Player_AzureBlockchainWorkBench_Player_AzureBlockchainWorkBench_NoBaseCtor(this, msgsender_MSG, msgvalue_MSG, pingPongGameName_s207);
}

procedure {:public} {:inline 1} Ping_Player_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, currentPingPongTimes_s225: int);
//ensures ((State_Starter[msgsender_MSG]) == (1) || (State_Starter[msgsender_MSG]) == (2));
ensures ( (State_Starter[msgsender_MSG]) == (2));
implementation Ping_Player_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, currentPingPongTimes_s225: int)
{
call Ping_Player(this, msgsender_MSG, msgvalue_MSG, currentPingPongTimes_s225);

call ContractUpdated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, -2012585696);
}

procedure {:public} {:inline 1} FinishGame_Player_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int);
implementation FinishGame_Player_AzureBlockchainWorkBench(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int)
{
call FinishGame_Player(this, msgsender_MSG, msgvalue_MSG);
call ContractUpdated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, 18166401);
}

var State_Starter: [Ref]int;
var PingPongGameName_Starter: [Ref]int;
var GameStarter_Starter: [Ref]Ref;
var GamePlayer_Starter: [Ref]Ref;
var PingPongTimes_Starter: [Ref]int;
procedure {:inline 1} Starter_Starter_NoBaseCtor(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, gameName_s284: int);
implementation Starter_Starter_NoBaseCtor(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, gameName_s284: int)
{
var __var_1: Ref;
var __var_2: int;
// start of initialization
assume ((msgsender_MSG) != (null));
PingPongGameName_Starter[this] := 720258860;
GameStarter_Starter[this] := null;
GamePlayer_Starter[this] := null;
PingPongTimes_Starter[this] := 0;
// end of initialization
PingPongGameName_Starter[this] := gameName_s284;
call  {:cexpr "PingPongGameName"} boogie_si_record_sol2Bpl_int(PingPongGameName_Starter[this]);
GameStarter_Starter[this] := msgsender_MSG;
call  {:cexpr "GameStarter"} boogie_si_record_sol2Bpl_ref(GameStarter_Starter[this]);
call __var_1 := FreshRefGenerator();
assume ((DType[__var_1]) == (Player_AzureBlockchainWorkBench));
call Player_AzureBlockchainWorkBench_Player_AzureBlockchainWorkBench(__var_1, this, __var_2, PingPongGameName_Starter[this]);
GamePlayer_Starter[this] := __var_1;
call  {:cexpr "GamePlayer"} boogie_si_record_sol2Bpl_ref(GamePlayer_Starter[this]);
State_Starter[this] := 0;
}

procedure {:constructor} {:public} {:inline 1} Starter_Starter(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, gameName_s284: int);
implementation Starter_Starter(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, gameName_s284: int)
{
call Starter_Starter_NoBaseCtor(this, msgsender_MSG, msgvalue_MSG, gameName_s284);
}

procedure {:public} {:inline 1} StartPingPong_Starter(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongTimes_s311: int);
implementation StartPingPong_Starter(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongTimes_s311: int)
{
var player_s311: Ref;
var __var_3: int;
PingPongTimes_Starter[this] := pingPongTimes_s311;
call  {:cexpr "PingPongTimes"} boogie_si_record_sol2Bpl_int(PingPongTimes_Starter[this]);
// begin
//assume ((DType[GamePlayer_Starter[this]]) == (Player));
assume ((DType[GamePlayer_Starter[this]]) == (Player) || (DType[GamePlayer_Starter[this]]) == (Player_AzureBlockchainWorkBench));
// end
player_s311 := GamePlayer_Starter[this];
State_Starter[this] := 1;
if ((DType[player_s311]) == (Player_AzureBlockchainWorkBench)) {

call Ping_Player_AzureBlockchainWorkBench(player_s311, this, __var_3, pingPongTimes_s311);
} else if ((DType[player_s311]) == (Player)) {
call Ping_Player(player_s311, this, __var_3, pingPongTimes_s311);
}

}

procedure {:public} {:inline 1} Pong_Starter(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, currentPingPongTimes_s3566: int);
implementation Pong_Starter(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, currentPingPongTimes_s3566: int)
{
var player_s356: Ref;
var __var_4: int;
var __var_5: int;
var currentPingPongTimes_s356: int;
currentPingPongTimes_s356 := (currentPingPongTimes_s3566) - (1);

call  {:cexpr "currentPingPongTimes"} boogie_si_record_sol2Bpl_int(currentPingPongTimes_s356);
// begin
//assume ((DType[GamePlayer_Starter[this]]) == (Player));
assume ((DType[GamePlayer_Starter[this]]) == (Player) || (DType[GamePlayer_Starter[this]]) == (Player_AzureBlockchainWorkBench));
// end
player_s356 := GamePlayer_Starter[this];

if ((currentPingPongTimes_s356) > (0)) {

State_Starter[this] := 1;
if ((DType[player_s356]) == (Player_AzureBlockchainWorkBench)) {
    call Ping_Player_AzureBlockchainWorkBench(player_s356, this, __var_4, currentPingPongTimes_s356);
} else if ((DType[player_s356]) == (Player)) {
    call Ping_Player(player_s356, this, __var_4, currentPingPongTimes_s356);
}
} else {
State_Starter[this] := 2;
if ((DType[player_s356]) == (Starter_AzureBlockchainWorkBench)) {
call FinishGame_Starter_AzureBlockchainWorkBench(player_s356, this, __var_5);
} else if ((DType[player_s356]) == (Starter)) {
call FinishGame_Starter(player_s356, this, __var_5);
} else if ((DType[player_s356]) == (Player_AzureBlockchainWorkBench)) {
call FinishGame_Player_AzureBlockchainWorkBench(player_s356, this, __var_5);
} else if ((DType[player_s356]) == (Player)) {
call FinishGame_Player(player_s356, this, __var_5);
}
}
}

procedure {:public} {:inline 1} FinishGame_Starter(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int);
implementation FinishGame_Starter(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int)
{
State_Starter[this] := 2;
}

var State_Player: [Ref]int;
var GameStarter_Player: [Ref]Ref;
var PingPongGameName_Player: [Ref]int;
procedure {:inline 1} Player_Player_NoBaseCtor(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongGameName_s396: int);
implementation Player_Player_NoBaseCtor(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongGameName_s396: int)
{
// start of initialization
assume ((msgsender_MSG) != (null));
GameStarter_Player[this] := null;
PingPongGameName_Player[this] := 720258860;
// end of initialization
GameStarter_Player[this] := msgsender_MSG;
call  {:cexpr "GameStarter"} boogie_si_record_sol2Bpl_ref(GameStarter_Player[this]);
PingPongGameName_Player[this] := pingPongGameName_s396;
call  {:cexpr "PingPongGameName"} boogie_si_record_sol2Bpl_int(PingPongGameName_Player[this]);
State_Player[this] := 0;
}

procedure {:constructor} {:public} {:inline 1} Player_Player(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongGameName_s396: int);
implementation Player_Player(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, pingPongGameName_s396: int)
{
call Player_Player_NoBaseCtor(this, msgsender_MSG, msgvalue_MSG, pingPongGameName_s396);
}

procedure {:public} {:inline 1} Ping_Player(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, currentPingPongTimes_s4422: int);
//ensures ((State_Starter[msgsender_MSG]) == (1) || (State_Starter[msgsender_MSG]) == (2)); //manually added
ensures ( (State_Starter[msgsender_MSG]) == (2)); //manually added

implementation Ping_Player(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int, currentPingPongTimes_s4422: int)
{
var starter_s442: Ref;
var __var_6: int;
var __var_7: int;
var currentPingPongTimes_s442: int;
currentPingPongTimes_s442 := (currentPingPongTimes_s4422) - (1);
call  {:cexpr "currentPingPongTimes"} boogie_si_record_sol2Bpl_int(currentPingPongTimes_s442);
// begin
//assume ((DType[msgsender_MSG]) == (Starter));
assume ((DType[msgsender_MSG]) == (Starter_AzureBlockchainWorkBench));
// end

starter_s442 := msgsender_MSG;
if ((currentPingPongTimes_s442) > (0)) {
State_Player[this] := 1;
if ((DType[starter_s442]) == (Starter_AzureBlockchainWorkBench)) {
call Pong_Starter_AzureBlockchainWorkBench(starter_s442, this, __var_6, currentPingPongTimes_s442);
} else if ((DType[starter_s442]) == (Starter)) {
call Pong_Starter(starter_s442, this, __var_6, currentPingPongTimes_s442);
}
} else {
State_Player[this] := 2;
if ((DType[starter_s442]) == (Starter_AzureBlockchainWorkBench)) {
call FinishGame_Starter_AzureBlockchainWorkBench(starter_s442, this, __var_7);
} else if ((DType[starter_s442]) == (Starter)) {
call FinishGame_Starter(starter_s442, this, __var_7);
} else if ((DType[starter_s442]) == (Player_AzureBlockchainWorkBench)) {
call FinishGame_Player_AzureBlockchainWorkBench(starter_s442, this, __var_7);
} else if ((DType[starter_s442]) == (Player_AzureBlockchainWorkBench)) {
call FinishGame_Player(starter_s442, this, __var_7);
}
}
}

procedure {:public} {:inline 1} FinishGame_Player(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int);
implementation FinishGame_Player(this: Ref, msgsender_MSG: Ref, msgvalue_MSG: int)
{
State_Player[this] := 2;
}

procedure BoogieEntry_WorkbenchBase();
implementation BoogieEntry_WorkbenchBase()
{
var this: Ref;
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var choice: int;
var applicationName_s40: int;
var workflowName_s40: int;
var action_s65: int;
assume ((((DType[this]) == (WorkbenchBase)) || ((DType[this]) == (Starter_AzureBlockchainWorkBench))) || ((DType[this]) == (Player_AzureBlockchainWorkBench)));
call WorkbenchBase_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, applicationName_s40, workflowName_s40);
while (true)
{
havoc msgsender_MSG;
havoc msgvalue_MSG;
havoc choice;
havoc applicationName_s40;
havoc workflowName_s40;
havoc action_s65;
if ((choice) == (1)) {
call ContractCreated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG);
} else if ((choice) == (2)) {
call ContractUpdated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, action_s65);
}
}
}

const {:existential true} HoudiniB1_Starter_AzureBlockchainWorkBench: bool;
const {:existential true} HoudiniB2_Starter_AzureBlockchainWorkBench: bool;
const {:existential true} HoudiniB3_Starter_AzureBlockchainWorkBench: bool;
const {:existential true} HoudiniB4_Starter_AzureBlockchainWorkBench: bool;
const {:existential true} HoudiniB5_Starter_AzureBlockchainWorkBench: bool;
const {:existential true} HoudiniB6_Starter_AzureBlockchainWorkBench: bool;
procedure BoogieEntry_Starter_AzureBlockchainWorkBench();
implementation BoogieEntry_Starter_AzureBlockchainWorkBench()
{
var this: Ref;
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var choice: int;
var applicationName_s40: int;
var workflowName_s40: int;
var action_s65: int;
var gameName_s284: int;
var pingPongTimes_s144: int;
var currentPingPongTimes_s162: int;
var gameName_s95: int;
var pingPongTimes_s126: int;
assume ((DType[this]) == (Starter_AzureBlockchainWorkBench));
call Starter_AzureBlockchainWorkBench_Starter_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, gameName_s95);
while (true)
/*
  invariant (HoudiniB1_Starter_AzureBlockchainWorkBench) ==> ((GameStarter_Starter[this]) == (null));
  invariant (HoudiniB2_Starter_AzureBlockchainWorkBench) ==> ((GameStarter_Starter[this]) != (null));
  invariant (HoudiniB3_Starter_AzureBlockchainWorkBench) ==> ((GamePlayer_Starter[this]) == (null));
  invariant (HoudiniB4_Starter_AzureBlockchainWorkBench) ==> ((GamePlayer_Starter[this]) != (null));
  invariant (HoudiniB5_Starter_AzureBlockchainWorkBench) ==> ((GameStarter_Starter[this]) == (GamePlayer_Starter[this]));
  invariant (HoudiniB6_Starter_AzureBlockchainWorkBench) ==> ((GameStarter_Starter[this]) != (GamePlayer_Starter[this]));
*/
{
havoc msgsender_MSG;
havoc msgvalue_MSG;
havoc choice;
havoc applicationName_s40;
havoc workflowName_s40;
havoc action_s65;
havoc gameName_s284;
havoc pingPongTimes_s144;
havoc currentPingPongTimes_s162;
havoc gameName_s95;
havoc pingPongTimes_s126;
if ((choice) == (1)) {
 call ContractCreated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG);
} else if ((choice) == (2)) {
 call ContractUpdated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, action_s65);
} else if ((choice) == (3)) {
 call FinishGame_Starter_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG);
} else if ((choice) == (4)) {
 call StartPingPong_Starter_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, pingPongTimes_s144);
} else if ((choice) == (5)) {
 call Pong_Starter_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, currentPingPongTimes_s162);
} else if ((choice) == (6)) {
 call Transition_GameProvisioned_Number_0_StartPingPong_Starter_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, pingPongTimes_s126);
}
}
}

const {:existential true} HoudiniB1_Player_AzureBlockchainWorkBench: bool;
const {:existential true} HoudiniB2_Player_AzureBlockchainWorkBench: bool;
procedure BoogieEntry_Player_AzureBlockchainWorkBench();
implementation BoogieEntry_Player_AzureBlockchainWorkBench()
{
var this: Ref;
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var choice: int;
var applicationName_s40: int;
var workflowName_s40: int;
var action_s65: int;
var pingPongGameName_s396: int;
var currentPingPongTimes_s225: int;
var pingPongGameName_s207: int;
assume ((DType[this]) == (Player_AzureBlockchainWorkBench));
call Player_AzureBlockchainWorkBench_Player_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, pingPongGameName_s207);
while (true)
  invariant (HoudiniB1_Player_AzureBlockchainWorkBench) ==> ((GameStarter_Player[this]) == (null));
  invariant (HoudiniB2_Player_AzureBlockchainWorkBench) ==> ((GameStarter_Player[this]) != (null));
{
havoc msgsender_MSG;
havoc msgvalue_MSG;
havoc choice;
havoc applicationName_s40;
havoc workflowName_s40;
havoc action_s65;
havoc pingPongGameName_s396;
havoc currentPingPongTimes_s225;
havoc pingPongGameName_s207;
if ((choice) == (1)) {
call ContractCreated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG);
} else if ((choice) == (2)) {
call ContractUpdated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, action_s65);
} else if ((choice) == (3)) {
call Ping_Player_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, currentPingPongTimes_s225);
} else if ((choice) == (4)) {
call FinishGame_Player_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG);
}
}
}

const {:existential true} HoudiniB1_Starter: bool;
const {:existential true} HoudiniB2_Starter: bool;
const {:existential true} HoudiniB3_Starter: bool;
const {:existential true} HoudiniB4_Starter: bool;
const {:existential true} HoudiniB5_Starter: bool;
const {:existential true} HoudiniB6_Starter: bool;
procedure BoogieEntry_Starter();
implementation BoogieEntry_Starter()
{
var this: Ref;
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var choice: int;
var gameName_s284: int;
var pingPongTimes_s311: int;
var currentPingPongTimes_s356: int;
assume (((DType[this]) == (Starter_AzureBlockchainWorkBench)) || ((DType[this]) == (Starter)));
call Starter_Starter(this, msgsender_MSG, msgvalue_MSG, gameName_s284);
while (true)
  invariant (HoudiniB1_Starter) ==> ((GameStarter_Starter[this]) == (null));
  invariant (HoudiniB2_Starter) ==> ((GameStarter_Starter[this]) != (null));
  invariant (HoudiniB3_Starter) ==> ((GamePlayer_Starter[this]) == (null));
  invariant (HoudiniB4_Starter) ==> ((GamePlayer_Starter[this]) != (null));
  invariant (HoudiniB5_Starter) ==> ((GameStarter_Starter[this]) == (GamePlayer_Starter[this]));
  invariant (HoudiniB6_Starter) ==> ((GameStarter_Starter[this]) != (GamePlayer_Starter[this]));
{
havoc msgsender_MSG;
havoc msgvalue_MSG;
havoc choice;
havoc gameName_s284;
havoc pingPongTimes_s311;
havoc currentPingPongTimes_s356;
if ((choice) == (1)) {
call FinishGame_Starter(this, msgsender_MSG, msgvalue_MSG);
} else if ((choice) == (2)) {
call StartPingPong_Starter(this, msgsender_MSG, msgvalue_MSG, pingPongTimes_s311);
} else if ((choice) == (3)) {
call Pong_Starter(this, msgsender_MSG, msgvalue_MSG, currentPingPongTimes_s356);
}
}
}

const {:existential true} HoudiniB1_Player: bool;
const {:existential true} HoudiniB2_Player: bool;
procedure BoogieEntry_Player();
implementation BoogieEntry_Player()
{
var this: Ref;
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var choice: int;
var pingPongGameName_s396: int;
var currentPingPongTimes_s442: int;
assume (((DType[this]) == (Player_AzureBlockchainWorkBench)) || ((DType[this]) == (Player)));
call Player_Player(this, msgsender_MSG, msgvalue_MSG, pingPongGameName_s396);
while (true)
  invariant (HoudiniB1_Player) ==> ((GameStarter_Player[this]) == (null));
  invariant (HoudiniB2_Player) ==> ((GameStarter_Player[this]) != (null));
{
havoc msgsender_MSG;
havoc msgvalue_MSG;
havoc choice;
havoc pingPongGameName_s396;
havoc currentPingPongTimes_s442;
if ((choice) == (1)) {
call Ping_Player(this, msgsender_MSG, msgvalue_MSG, currentPingPongTimes_s442);
} else if ((choice) == (2)) {
call FinishGame_Player(this, msgsender_MSG, msgvalue_MSG);
}
}
}

procedure CorralChoice_WorkbenchBase(this: Ref);
implementation CorralChoice_WorkbenchBase(this: Ref)
{
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var choice: int;
var applicationName_s40: int;
var workflowName_s40: int;
var action_s65: int;
havoc msgsender_MSG;
havoc msgvalue_MSG;
havoc choice;
havoc applicationName_s40;
havoc workflowName_s40;
havoc action_s65;
if ((choice) == (1)) {
call ContractCreated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG);
} else if ((choice) == (2)) {
call ContractUpdated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, action_s65);
}
}

procedure CorralEntry_WorkbenchBase();
implementation CorralEntry_WorkbenchBase()
{
var this: Ref;
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var applicationName_s40: int;
var workflowName_s40: int;
assume ((((DType[this]) == (WorkbenchBase)) || ((DType[this]) == (Starter_AzureBlockchainWorkBench))) || ((DType[this]) == (Player_AzureBlockchainWorkBench)));
call WorkbenchBase_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, applicationName_s40, workflowName_s40);
while (true)
{
call CorralChoice_WorkbenchBase(this);
}
}

procedure CorralChoice_Starter_AzureBlockchainWorkBench(this: Ref);
implementation CorralChoice_Starter_AzureBlockchainWorkBench(this: Ref)
{
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var choice: int;
var applicationName_s40: int;
var workflowName_s40: int;
var action_s65: int;
var gameName_s284: int;
var pingPongTimes_s144: int;
var currentPingPongTimes_s162: int;
var gameName_s95: int;
var pingPongTimes_s126: int;
havoc msgsender_MSG;
havoc msgvalue_MSG;
havoc choice;
havoc applicationName_s40;
havoc workflowName_s40;
havoc action_s65;
havoc gameName_s284;
havoc pingPongTimes_s144;
havoc currentPingPongTimes_s162;
havoc gameName_s95;
havoc pingPongTimes_s126;
if ((choice) == (1)) {
call ContractCreated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG);
} else if ((choice) == (2)) {
call ContractUpdated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, action_s65);
} else if ((choice) == (3)) {
call FinishGame_Starter_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG);
} else if ((choice) == (4)) {
call StartPingPong_Starter_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, pingPongTimes_s144);
} else if ((choice) == (5)) {
call Pong_Starter_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, currentPingPongTimes_s162);
} else if ((choice) == (6)) {
call Transition_GameProvisioned_Number_0_StartPingPong_Starter_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, pingPongTimes_s126);
}
}

procedure CorralEntry_Starter_AzureBlockchainWorkBench();
implementation CorralEntry_Starter_AzureBlockchainWorkBench()
{
var this: Ref;
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var gameName_s95: int;
assume ((DType[this]) == (Starter_AzureBlockchainWorkBench));
call Starter_AzureBlockchainWorkBench_Starter_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, gameName_s95);
while (true)
{
call CorralChoice_Starter_AzureBlockchainWorkBench(this);
}
}

procedure CorralChoice_Player_AzureBlockchainWorkBench(this: Ref);
implementation CorralChoice_Player_AzureBlockchainWorkBench(this: Ref)
{
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var choice: int;
var applicationName_s40: int;
var workflowName_s40: int;
var action_s65: int;
var pingPongGameName_s396: int;
var currentPingPongTimes_s225: int;
var pingPongGameName_s207: int;
havoc msgsender_MSG;
havoc msgvalue_MSG;
havoc choice;
havoc applicationName_s40;
havoc workflowName_s40;
havoc action_s65;
havoc pingPongGameName_s396;
havoc currentPingPongTimes_s225;
havoc pingPongGameName_s207;
if ((choice) == (1)) {
call ContractCreated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG);
} else if ((choice) == (2)) {
call ContractUpdated_WorkbenchBase(this, msgsender_MSG, msgvalue_MSG, action_s65);
} else if ((choice) == (3)) {
call Ping_Player_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, currentPingPongTimes_s225);
} else if ((choice) == (4)) {
call FinishGame_Player_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG);
}
}

procedure CorralEntry_Player_AzureBlockchainWorkBench();
implementation CorralEntry_Player_AzureBlockchainWorkBench()
{
var this: Ref;
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var pingPongGameName_s207: int;
assume ((DType[this]) == (Player_AzureBlockchainWorkBench));
call Player_AzureBlockchainWorkBench_Player_AzureBlockchainWorkBench(this, msgsender_MSG, msgvalue_MSG, pingPongGameName_s207);
while (true)
{
call CorralChoice_Player_AzureBlockchainWorkBench(this);
}
}

procedure CorralChoice_Starter(this: Ref);
implementation CorralChoice_Starter(this: Ref)
{
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var choice: int;
var gameName_s284: int;
var pingPongTimes_s311: int;
var currentPingPongTimes_s356: int;
havoc msgsender_MSG;
havoc msgvalue_MSG;
havoc choice;
havoc gameName_s284;
havoc pingPongTimes_s311;
havoc currentPingPongTimes_s356;
if ((choice) == (1)) {
call FinishGame_Starter(this, msgsender_MSG, msgvalue_MSG);
} else if ((choice) == (2)) {
call StartPingPong_Starter(this, msgsender_MSG, msgvalue_MSG, pingPongTimes_s311);
} else if ((choice) == (3)) {
call Pong_Starter(this, msgsender_MSG, msgvalue_MSG, currentPingPongTimes_s356);
}
}

procedure CorralEntry_Starter();
implementation CorralEntry_Starter()
{
var this: Ref;
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var gameName_s284: int;
assume (((DType[this]) == (Starter_AzureBlockchainWorkBench)) || ((DType[this]) == (Starter)));
call Starter_Starter(this, msgsender_MSG, msgvalue_MSG, gameName_s284);
while (true)
{
call CorralChoice_Starter(this);
}
}

procedure CorralChoice_Player(this: Ref);
implementation CorralChoice_Player(this: Ref)
{
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var choice: int;
var pingPongGameName_s396: int;
var currentPingPongTimes_s442: int;
havoc msgsender_MSG;
havoc msgvalue_MSG;
havoc choice;
havoc pingPongGameName_s396;
havoc currentPingPongTimes_s442;
if ((choice) == (1)) {
call Ping_Player(this, msgsender_MSG, msgvalue_MSG, currentPingPongTimes_s442);
} else if ((choice) == (2)) {
call FinishGame_Player(this, msgsender_MSG, msgvalue_MSG);
}
}

procedure CorralEntry_Player();
implementation CorralEntry_Player()
{
var this: Ref;
var msgsender_MSG: Ref;
var msgvalue_MSG: int;
var pingPongGameName_s396: int;
assume (((DType[this]) == (Player_AzureBlockchainWorkBench)) || ((DType[this]) == (Player)));
call Player_Player(this, msgsender_MSG, msgvalue_MSG, pingPongGameName_s396);
while (true)
{
call CorralChoice_Player(this);
}
}


