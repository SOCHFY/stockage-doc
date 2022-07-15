
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Event
{

  [JsonPropertyName("trace_id")]
  public string TraceId { get; set; }

  [JsonPropertyName("span_id")]
  public string SpanId { get; set; }

  [JsonPropertyName("parent_span_id")]
  public string ParentSpanId { get; set; }

  [JsonPropertyName("span_type")]
  public string SpanType { get; set; }

  [JsonPropertyName("event_id")]
  public string EventId { get; set; }

  [JsonPropertyName("event_type")]
  public string EventType { get; set; }

  [JsonPropertyName("event_detail")]
  public string EventDetail { get; set; }

  [JsonPropertyName("domain_name")]
  public string DomainName { get; set; }

  [JsonPropertyName("timestamp")]
  public DateTime Timestamp { get; set; }

  [JsonPropertyName("processor")]
  public Processor Processor { get; set; }

  [JsonPropertyName("batches")]
  public List<Batch> Batches { get; set; }

  [JsonPropertyName("folds")]
  public List<Fold> Folds { get; set; }

  [JsonPropertyName("documents")]
  public List<Document> Documents { get; set; }

  [JsonPropertyName("sheets")]
  public List<Sheet> Sheets { get; set; }

  [JsonPropertyName("pages")]
  public List<Page> Pages { get; set; }

  [JsonPropertyName("fields")]
  public List<Field> Fields { get; set; }
  
   public override string ToString()
    {
        return "Event_id : " + EventId + " , event_type : " + EventType;
    }

}
public class Batch
{
  [JsonPropertyName("id")]
  public string Id { get; set; }

  [JsonPropertyName("customer_id")]
  public string CustomerId { get; set; }

  [JsonPropertyName("business_id")]
  public string BusinessId { get; set; }

  [JsonPropertyName("site_id")]
  public string SiteId { get; set; }
}

public class Document
{
  [JsonPropertyName("id")]
  public string Id { get; set; }

  [JsonPropertyName("type")]
  public string Type { get; set; }

  [JsonPropertyName("title")]
  public string Title { get; set; }

  [JsonPropertyName("previous_type")]
  public string PreviousType { get; set; }

  [JsonPropertyName("fold_id")]
  public string FoldId { get; set; }
}

public class Field
{
  [JsonPropertyName("id")]
  public string Id { get; set; }

  [JsonPropertyName("label")]
  public string Label { get; set; }

  [JsonPropertyName("type")]
  public string Type { get; set; }

  [JsonPropertyName("previous_type")]
  public object PreviousType { get; set; }

  [JsonPropertyName("value")]
  public string Value { get; set; }

  [JsonPropertyName("previous_value")]
  public object PreviousValue { get; set; }

  [JsonPropertyName("page_id")]
  public string PageId { get; set; }
}

public class Fold
{
  [JsonPropertyName("id")]
  public string Id { get; set; }

  [JsonPropertyName("batch_id")]
  public string BatchId { get; set; }
}

public class Page
{
  [JsonPropertyName("id")]
  public string Id { get; set; }

  [JsonPropertyName("type")]
  public string Type { get; set; }

  [JsonPropertyName("previous_type")]
  public object PreviousType { get; set; }

  [JsonPropertyName("sheet_id")]
  public string SheetId { get; set; }
}

public class Processor
{
  [JsonPropertyName("id")]
  public string Id { get; set; }

  [JsonPropertyName("type")]
  public string Type { get; set; }

  [JsonPropertyName("bank_code")]
  public string BankCode { get; set; }

  [JsonPropertyName("branch_code")]
  public string BranchCode { get; set; }

  [JsonPropertyName("office_code")]
  public string OfficeCode { get; set; }

  [JsonPropertyName("ip")]
  public string Ip { get; set; }
}

public class Sheet
{
  [JsonPropertyName("id")]
  public string Id { get; set; }

  [JsonPropertyName("document_id")]
  public string DocumentId { get; set; }
}

