// SPDX-License-Identifier: MIT
/* Code generated by compiler */
library Safe_Arith {

    function safe_add (uint a, uint b) public pure returns (uint) {
        if (a > a + b) revert ("<Safe_Arith.add> Overflow Error");
        else return (a + b);
    }
    
    function safe_sub (uint a, uint b) public pure returns (uint) {
        if (a < b) revert ("<Safe_Arith.sub> Underflow Error");
        else return (a - b);
    }
    
    function safe_mul (uint a, uint b) public pure returns (uint) {
        if (b == 0) return 0;
        if (a != (a * b)/b) revert ("<Safe_Arith.mul> Overflow Error");
        else return (a * b);
    }
    
    function safe_div (uint a, uint b) public pure returns (uint) {
        if (b == 0) revert ("<Safe_Arith.div> Division by 0 error");
        else return (a / b);
    }
}