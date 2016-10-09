using System;

namespace ToyCompiler.Scanner
{
    /// <summary>
    /// 语法声明枚举
    /// </summary>
    public enum NodeKind
    {
        /// <summary>
        /// 词法转换单元
        /// </summary>
        NK_TranslationUnit,
        /// <summary>
        /// 函数
        /// </summary>
        NK_Function,
        /// <summary>
        /// 声明
        /// </summary>
        NK_Declaration,
        /// <summary>
        /// 类型名称
        /// </summary>
        NK_TypeName,
        /// <summary>
        /// 声明限定符
        /// </summary>
        NK_Specifiers,
        /// <summary>
        /// 词法单元
        /// </summary>
        NK_Token,
        /// <summary>
        /// typedef名称
        /// </summary>
        NK_TypedefName,

        /// <summary>
        /// 枚举限定符
        /// </summary>
        NK_EnumSpecifier,
        /// <summary>
        /// 枚举值
        /// </summary>
        NK_Enumerator,
        /// <summary>
        /// 结构体限定符
        /// </summary>
        NK_StructSpecifier,
        /// <summary>
        /// 联合限定符
        /// </summary>
        NK_UnionSpecifier,
        /// <summary>
        /// 结构体声明
        /// </summary>
        NK_StructDeclaration,
        /// <summary>
        /// 结构体声明符
        /// </summary>
        NK_StructDeclarator,
        /// <summary>
        /// 指针声明符
        /// </summary>
        NK_PointerDeclarator,
        /// <summary>
        /// 数组声明符
        /// </summary>
        NK_ArrayDeclarator,

        /// <summary>
        /// 函数声明符
        /// </summary>
        NK_FunctionDeclarator,

        /// <summary>
        /// 参数类型列表
        /// </summary>
        NK_ParameterTypeList,

        /// <summary>
        /// 参数声明
        /// </summary>
        NK_ParameterDeclaration,

        /// <summary>
        /// 名称声明符
        /// </summary>
        NK_NameDeclarator,

        /// <summary>
        /// 初始化声明符
        /// </summary>
        NK_InitDeclarator,
        /// <summary>
        /// 初始化
        /// </summary>
        NK_Initializer,

        /// <summary>
        /// 表达式
        /// </summary>
        NK_Expression,

        /// <summary>
        /// 表达式语句
        /// </summary>
        NK_ExpressionStatement,

        /// <summary>
        /// 标签语句
        /// </summary>
        NK_LabelStatement,

        /// <summary>
        /// case语句
        /// </summary>
        NK_CaseStatement,
        /// <summary>
        /// 默认值语句
        /// </summary>
        NK_DefaultStatement,
        /// <summary>
        /// if语句
        /// </summary>
        NK_IfStatement,
        /// <summary>
        /// switch语句
        /// </summary>
        NK_SwitchStatement,

        /// <summary>
        /// while语句
        /// </summary>
        NK_WhileStatement,

        /// <summary>
        /// do语句
        /// </summary>
        NK_DoStatement,

        /// <summary>
        /// for语句
        /// </summary>
        NK_ForStatement,

        /// <summary>
        /// goto语句
        /// </summary>
        NK_GotoStatement,

        /// <summary>
        /// break语句
        /// </summary>
        NK_BreakStatement,

        /// <summary>
        /// continue语句
        /// </summary>
        NK_ContinueStatement,

        /// <summary>
        /// return语句
        /// </summary>
        NK_ReturnStatement,

        /// <summary>
        /// 复合语句
        /// </summary>
        NK_CompoundStatement
    }
}
