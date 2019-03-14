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
            //if (modifier.Parameters.Length() > 0)
            //{
            //    throw new System.Exception("modifiers with parameters not implemented");
            //}
            var modifierInParams = TransUtils.GetDefaultInParams();
            
            foreach (var parameter in modifier.Parameters.Parameters)
            {
                string name = null;
                name = TransUtils.GetCanonicalLocalVariableName(parameter);
                BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(parameter.TypeName);
                modifierInParams.Add(new BoogieFormalParam(new BoogieTypedIdent(name, type)));
            }

            Block body = modifier.Body;
            bool hasPre = false;
            bool hasPost = false;
            List<Statement> postlude = new List<Statement>();

            bool translatingPre = true;
            foreach (Statement statement in body.Statements)
            {
                if (statement is VariableDeclarationStatement)
                {
                    throw new System.Exception("locals within modifiers not supported");
                }
                if (statement is PlaceholderStatement)
                {
                    translatingPre = false;
                    continue;
                }
                if (translatingPre)
                {
                    hasPre = true;
                }
                else
                {
                    hasPost = true;
                }
            }

            if (hasPre)
            {
                List<BoogieVariable> inParams = modifierInParams;
                List<BoogieVariable> outParams = new List<BoogieVariable>();
                BoogieProcedure preludeProc = new BoogieProcedure(modifier.Name + "_pre", inParams, outParams);
                context.AddModiferToPreProc(modifier.Name, preludeProc);

                BoogieImplementation preludeImpl = new BoogieImplementation(modifier.Name + "_pre", 
                    inParams, outParams, new List<BoogieVariable>(), new BoogieStmtList());
                context.AddModiferToPreImpl(modifier.Name, preludeImpl);
            }

            if (hasPost)
            {
                List<BoogieVariable> inParams = modifierInParams;
                List<BoogieVariable> outParams = new List<BoogieVariable>();
                BoogieProcedure postludeProc = new BoogieProcedure(modifier.Name + "_post", inParams, outParams);
                context.AddModiferToPostProc(modifier.Name, postludeProc);

                BoogieImplementation postludeImpl = new BoogieImplementation(modifier.Name + "_post",
                    inParams, outParams, new List<BoogieVariable>(), new BoogieStmtList());
                context.AddModiferToPostImpl(modifier.Name, postludeImpl);
            }

            return false;
        }
    }
}
