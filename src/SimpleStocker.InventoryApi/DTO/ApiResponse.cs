namespace SimpleStocker.InventoryApi.DTO
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; } = false;                          // Indica sucesso ou falha
        public string Message { get; private set; } = "";                           // Mensagem amigável para o usuário
        public List<Dictionary<string, string>> Errors { get; private set; } = [];  // Lista de erros, se houver
        public T Data { get; private set; } = default;                              // Objeto ou lista de dados retornados
        public int StatusCode { get; private set; } = 0;                            // Código de status HTTP

        public ApiResponse()
        {
            Errors = [];
        }

        public ApiResponse(bool success, string message, List<Dictionary<string, string>> errors, T data, int statusCode)
        {
            Success = success;
            Message = message;
            Errors = errors;
            Data = data;
            StatusCode = statusCode;
        }

        public ApiResponse(List<Dictionary<string, string>> errors)
        {
            Success = false;
            Message = "";
            Errors = errors;
            Data = default;
            StatusCode = 400;
        }
        public ApiResponse(string field, string error)
        {
            Success = false;
            Message = "";
            Errors.Add(new Dictionary<string, string> { { field, error } });
            Data = default;
            StatusCode = 400;
        }

        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
            Errors = [];
            Data = default;
            StatusCode = 400;
        }

        // Sucesso (200 ou outro)
        public static ApiResponse<T> SuccessResponse(T data, string message = "Operação realizada com sucesso", int statusCode = 200)
        {
            return new ApiResponse<T>(true, message, [], data, statusCode);
        }

        // Erro 400 - Requisição malformada, validação etc.
        public static ApiResponse<T> BadRequestResponse(List<Dictionary<string, string>> errors, T model)
        {
            return new ApiResponse<T>(false, "", errors ?? new List<Dictionary<string, string>>
                    {
                        new() { { "Server", "Erro de validação ou parâmetros incorretos." } }
                    }, model, 400);
        }

        // Erro 500 - Erro interno
        public static ApiResponse<T> InternalServerErrorResponse(Exception ex, T model)
        {
            return new ApiResponse<T>(false, "Erro interno do servidor: " + ex.Message, [], model, 500);
        }
    }
}
