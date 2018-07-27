var boolValidacije = true;   
   function Validacije() {
   
       boolValidacije = true;
   
       $("#status").next().children(".selection").children(0).css("border-color", "")
       $("#category").next().children(".selection").children(0).css("border-color", "");
       $("#sysSection").next().children(".selection").children(0).css("border-color", "");
       $("#severity").next().children(".selection").children(0).css("border-color", "");
       $("#priority").next().children(".selection").children(0).css("border-color", "");
       $("#title").css("border-color", "");
       $("#dueOn").css("border-color", "");

       $("#steps").css("border-color", "");
       $("#notes").css("border-color", "");
   
       $("#status").siblings("span.redcolor").remove();
       $("#category").siblings("span.redcolor").remove();
       $("#sysSection").siblings("span.redcolor").remove();
       $("#severity").siblings("span.redcolor").remove();
       $("#priority").siblings("span.redcolor").remove();
       $("#title").siblings("span.redcolor").remove();
       $("#dueOn").siblings("span.redcolor").remove();

       $("#steps").siblings("span.redcolor").remove();
       $("#notes").siblings("span.redcolor").remove();
   
       if ($("#status").val() === null) {
           $("<span>Choose status!</span>").appendTo($("#status").parent()).addClass("redcolor");
           $("#status").next().children(".selection").children(0).css("border-color", "red");
           boolValidacije = false;
       }
   
       if ($("#dueOn").children(0).children(0).val() === "") {
           $("<span>Choose Due Date!</span>").appendTo($("#dueOn").parent()).addClass("redcolor");
           $("#dueOn").css("border-color", "red");
           boolValidacije = false;
       }
   
       if ($("#category").val() === null) {
   
           $("<span>Choose category!</span>").appendTo($("#category").parent()).addClass("redcolor");
           $("#category").next().children(".selection").children(0).css("border-color", "red");
           boolValidacije = false;
       }
   
       if ($("#sysSection").val() === null) {
           $("<span>Choose section!</span>").appendTo($("#sysSection").parent()).addClass("redcolor");
           $("#sysSection").next().children(".selection").children(0).css("border-color", "red");
           boolValidacije = false;
       }
   
       if ($("#severity").val() === null) {
           $("<span>Choose severity!</span>").appendTo($("#severity").parent()).addClass("redcolor");
           $("#severity").next().children(".selection").children(0).css("border-color", "red");
           boolValidacije = false;
       }
   
       if ($("#priority").val() === null) {
   
           $("<span>Choose priority!</span>").appendTo($("#priority").parent()).addClass("redcolor");
           $("#priority").next().children(".selection").children(0).css("border-color", "red");
           boolValidacije = false;
       }
   
       if ($("#title").val().length < 3) {
           $("<span>Must have 3 or more characters!</span>").appendTo($("#title").parent()).addClass("redcolor");
           $("#title").css("border-color", "red");
           boolValidacije = false;
       }

       if ($("#steps").val().length < 1) {
           $("<span>Steps cannot be empty!</span>").appendTo($("#steps").parent()).addClass("redcolor");
           $("#steps").css("border-color", "red");
           boolValidacije = false;
       }

       if ($("#notes").val().length < 1) {
           $("<span>Notes cannot be empty!</span>").appendTo($("#notes").parent()).addClass("redcolor");
           $("#notes").css("border-color", "red");
           boolValidacije = false;
       }

   }
