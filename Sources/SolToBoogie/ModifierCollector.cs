// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using SolidityAST;
    using BoogieAST;
    using System.Collections.Generic;
    using System;
    using System.Security.Cryptography.X509Certificates;
    using System.Linq;

    /**
     * Collect all modifier definitions and put them in the translator context.
     */
    public class ModifierCollector : BasicASTVisitor
    {
        private TranslatorContext context;

        public ModifierCollector(TranslatorContext context)
        {
            this.context = context;
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
                name = TransUtils.GetCanonicalLocalVariableName(parameter, context);
                BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(parameter.TypeName);
                modifierInParams.Add(new BoogieFormalParam(new BoogieTypedIdent(name, type)));
            }

            Block body = modifier.Body;
            bool hasPre = false;
            bool hasPost = false;
            List<Statement> postlude = new List<Statement>();

            // this list does not include locals introduced during translation
            List<BoogieLocalVariable> localVarsDeclared = new List<BoogieLocalVariable>();

            bool translatingPre = true;
            foreach (Statement statement in body.Statements)
            {
                if (statement is VariableDeclarationStatement varDecls)
                {
                    foreach (VariableDeclaration varDecl in varDecls.Declarations)
                    {
                        string name = TransUtils.GetCanonicalLocalVariableName(varDecl, context);
                        BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(varDecl.TypeName);
                        // Issue a warning for intXX variables in case /useModularArithemtic option is used:
                        if (context.TranslateFlags.UseModularArithmetic && varDecl.TypeDescriptions.IsInt())
                        {
                            Console.WriteLine($"Warning: signed integer arithmetic is not handled with /useModularArithmetic option");
                        }
                        var boogieVariable = new BoogieLocalVariable(new BoogieTypedIdent(name, type));
                        if (translatingPre)
                            localVarsDeclared.Add(boogieVariable); // don't add locals after placeholder
                    }

                    // throw new System.Exception("locals within modifiers not supported");
                }
                if (statement is PlaceholderStatement)
                {
                    translatingPre = false;
                    // only capture those locals declared in the prefix, these are visible to postlude
                    context.AddPreludeLocalsToModifier(modifier.Name, localVarsDeclared);

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

            var attributes = new List<BoogieAttribute>();
            attributes.Add(new BoogieAttribute("inline", 1));

            if (hasPre)
            {
                List<BoogieVariable> inParams = new List<BoogieVariable>(modifierInParams);
                List<BoogieVariable> outParams = new List<BoogieVariable>();
                outParams.AddRange(localVarsDeclared.Select(x => new BoogieFormalParam(new BoogieTypedIdent("__out_mod_" + x.Name, x.TypedIdent.Type))));
                BoogieProcedure preludeProc = new BoogieProcedure(modifier.Name + "_pre", inParams, outParams, attributes);
                context.AddModiferToPreProc(modifier.Name, preludeProc);

                BoogieImplementation preludeImpl = new BoogieImplementation(modifier.Name + "_pre", 
                    inParams, outParams, new List<BoogieVariable>(), new BoogieStmtList());
                context.AddModiferToPreImpl(modifier.Name, preludeImpl);
            }

            if (hasPost)
            {
                List<BoogieVariable> inParams = new List<BoogieVariable>(modifierInParams);
                inParams.AddRange(localVarsDeclared);
                List<BoogieVariable> outParams = new List<BoogieVariable>();
                BoogieProcedure postludeProc = new BoogieProcedure(modifier.Name + "_post", inParams, outParams, attributes);
                context.AddModiferToPostProc(modifier.Name, postludeProc);

                BoogieImplementation postludeImpl = new BoogieImplementation(modifier.Name + "_post",
                    inParams, outParams, new List<BoogieVariable>(), new BoogieStmtList());
                context.AddModiferToPostImpl(modifier.Name, postludeImpl);
            }

            return false;
        }
    }
}
