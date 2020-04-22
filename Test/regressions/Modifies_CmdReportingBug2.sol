pragma solidity ^0.5.0;

// Tests different types of mappings as arguments for VeriSol.Modifies
// TODO: postconditions on lines 27 and 28 are passing according to the cmd line and boogie.txt; 
// (wrong result), no corral.txt is generated

import "SafeMath.sol";
import "./Libraries/VeriSolContracts.sol";

contract Test {
	using SafeMath for uint256;
	
	mapping (address => uint256) public mapUint256;
	mapping (address => int256) public mapInt256;
	mapping (address => bool) public mapBool;
	
	constructor() public {}
	
	function modBool(address a, address b) public {	
		mapBool[a] = !mapBool[a];
		mapBool[b] = !mapBool[b];
	}
	
	function modBoolWrapper(address a, address b, bool c) public {
		modBool(a, b);
		VeriSol.Modifies(mapBool, [a, b]);
		VeriSol.Modifies(mapBool, [a]);     //TODO: passes - why?
		VeriSol.Modifies(mapBool, [b]);     //TODO: passes - why?
	}
}