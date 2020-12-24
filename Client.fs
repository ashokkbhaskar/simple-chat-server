namespace simple_chat_server

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html

[<JavaScript>]
module Client =

    let Main () =
        let rvInput = Var.Create ""
        let submit = Submitter.CreateOption rvInput.View
        let vReversed =
            submit.View.MapAsync(function
                | None -> async { return "" }
                | Some input -> Server.DoSomething input
            )
        let vLength = vReversed.Map(function
            | "" -> "The server response is empty."
            | s -> "The server response has " + (String.length s |> string) + " characters."
        )
        div [] [
            Doc.Input [] rvInput
            Doc.Button "Send" [] submit.Trigger
            hr [] []
            h4 [attr.``class`` "text-muted"] [text "The server responded:"]
            div [attr.``class`` "jumbotron"] [
                h1 [] [textView vReversed]
                h2 [] [textView vLength]
            ]
        ]
