namespace TrainingBackend.Exceptions;

/// <summary>対象リソースが存在しないときに投げる。HTTP 404 に対応。</summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}

/// <summary>業務ルール違反（在庫不足・不正なクーポンなど）のときに投げる。HTTP 400 に対応。</summary>
public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message)
    {
    }
}
