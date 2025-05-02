using System.Reflection;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleStocker.Api.Models.ViewModels
{
    public class ApiResponse<T>
    {
        private bool Success { get; set; }               // Indica sucesso ou falha
        private string Message { get; set; }             // Mensagem amigável para o usuário
        private List<string> Errors { get; set; }        // Lista de erros, se houver
        private T Data { get; set; }                     // Objeto ou lista de dados retornados
        private int StatusCode { get; set; }             // Código de status HTTP

        public ApiResponse()
        {
            Errors = new List<string>();
        }

        public ApiResponse(bool success, string message, List<string> errors, T data, int statusCode)
        {
            Success = success;
            Message = message;
            Errors = errors;
            Data = data;
            StatusCode = statusCode;
        }

        // Sucesso (200 ou outro)
        public static ApiResponse<T> SuccessResponse(T data, string message = "Operação realizada com sucesso", int statusCode = 200)
        {
            return new ApiResponse<T>(true, message, [], data, statusCode);
        }

        // Erro 400 - Requisição malformada, validação etc.
        public static ApiResponse<T> BadRequestResponse(List<string> errors, T model)
        {
            return new ApiResponse<T>(false, "", errors ?? ["Erro de validação ou parâmetros incorretos."],model,400);
        }

        // Erro 500 - Erro interno
        public static ApiResponse<T> InternalServerErrorResponse(Exception ex, T model)
        {   
            return new ApiResponse<T>(false,"Erro interno do servidor: " + ex.Message, [],model,500);
        }
    }
}
