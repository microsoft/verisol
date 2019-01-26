// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using SolidityAST;
    using BoogieAST;
    using System.Collections.Generic;

    /**
     * Collect all modifier definitions and put them in the translator context.
     */
    public class ModifierCollector : BasicASTVisitor
    {
        private TranslatorContext context;
        private ProcedureTranslator localTranslator;

        public ModifierCollector(TranslatorContext context)
        {
            this.context = context;
            this.localTranslator = new ProcedureTranslator(context);
        }

        public override bool Visit(ModifierDefinition modifier)
        {
            Block body = modifier.Body;
            BoogieStmtList prelude = new BoogieStmtList();
            BoogieStmtList postlude = new BoogieStmtList();

            bool translatingPre = true;
            foreach (Statement statement in body.Statements)
            {
                if (statement is PlaceholderStatement)
                {
                    translatingPre = false;
                    continue;
                }
                BoogieStmtList stmtList = localTranslator.TranslateStatement(statement);
                if (translatingPre)
                {
                    prelude.AppendStmtList(stmtList);
                }
                else
                {
                    postlude.AppendStmtList(stmtList);
                }
            }

            if (prelude.StatementCount() > 0)
            {
                List<BoogieVariable> inParams = TransUtils.GetInParams();
                List<BoogieVariable> outParams = new List<BoogieVariable>();
                BoogieProcedure preludeProc = new BoogieProcedure(modifier.Name + "_pre", inParams, outParams);
                context.AddModiferToPreProc(modifier.Name, preludeProc);

                inParams = TransUtils.GetInParams();
                outParams = new List<BoogieVariable>();
                BoogieImplementation preludeImpl = new BoogieImplementation(modifier.Name + "_pre", 
                    inParams, outParams, new List<BoogieVariable>(), prelude);
                context.AddModiferToPreImpl(modifier.Name, preludeImpl);
            }

            if (postlude.StatementCount() > 0)
            {
                List<BoogieVariable> inParams = TransUtils.GetInParams();
                List<BoogieVariable> outParams = new List<BoogieVariable>();
                BoogieProcedure postludeProc = new BoogieProcedure(modifier.Name + "_post", inParams, outParams);
                context.AddModiferToPostProc(modifier.Name, postludeProc);

                inParams = TransUtils.GetInParams();
                outParams = new List<BoogieVariable>();
                BoogieImplementation postludeImpl = new BoogieImplementation(modifier.Name + "_post",
                    inParams, outParams, new List<BoogieVariable>(), postlude);
                context.AddModiferToPostImpl(modifier.Name, postludeImpl);
            }

            return false;
        }
    }
}
