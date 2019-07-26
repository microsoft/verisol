pragma solidity >=0.4.24<0.6.0;

/**
 * Library for VeriSol Code Contracts
 * 
 * Mechanism to add various specification constructs
 *   loop invariants
 *   contract invariants
 *   precondition
 *   postcondition
 *   
 * Also support enriching the assertion language 
 * 
 */
library VeriSol {

   /*
    ********************************************************************
    *  Specification mechanisms
    ********************************************************************
    */

    /**
     * Loop invariant
     *
     * Calling this function within a loop VeriSol.Invariant(I) installs
     * I as a loop invariant. I should only refer to variables in scope
     * at the loop entry, and evaluated at the loop head. 
     * 
     * Using "Invariant" to avoid clash with a potential "invariant" keyword in Solidity
     * to directly support loop invariants https://github.com/ethereum/solidity/issues/6210
     */
    function Invariant(bool b) internal pure {
    }

    /**
     * Contract invariant
     * 
     * 
     * Calling this function within exactly one view function 
     * VeriSol.ContractInvariant(I) installs I  as a loop invariant
     * for the harness that calls public methods in the contract in a 
     * loop. See https://arxiv.org/abs/1812.08829 (Sec IV A)
     *
     * It is currently not inherited by derived contracts
     *
     * I should only refer to variables in global scope i.e. state variables.
     * 
     */
    function ContractInvariant(bool b) internal pure {
    }


   /*
    ********************************************************************
    *  New functions for extending assertion language
    ********************************************************************
    */

    /**
     * A new in-built function that returns the sum of all values of a mapping
     * 
     */  
    function SumMapping(mapping (address => uint256) storage a) internal pure returns (uint256){
       return 0; // The body is ignored
    }

    /**
     * A new in-built function that returns the sum of all values of a mapping
     * 
     */  
    function Old(uint x) internal pure returns (uint){
       return 0; // The body is ignored
    }
    function Old(address x) internal pure returns (address){
       return address(0x0); // The body is ignored
    }
    

}