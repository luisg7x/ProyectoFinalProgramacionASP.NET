$(document).ready(function(){
    $("#tarjeta").hide();
    $("#cantidad1").hide();
    
		var corre = $("#correo1").html();
		$("#correo").val(corre);
                var tarj = $("#tarjeta1").html();
		$("#tarjeta").val(tarj);
                var cant = $("#cantidad1").html();
		$("#cantidad").val(cant);
                

	$("#tarjeta1").click(function(){
		$(this).hide();
		var num = $(this).html();
		$("#tarjeta").val(num).show();
	});

	$("#tarjeta").blur(function(){
		$(this).hide();
		var cantidad = $(this).val();
		$("#tarjeta1").html(cantidad).show();
	});
        
        
        
    $("#correo").hide();

	$("#correo1").click(function(){
		$(this).hide();
		var corr = $(this).html();
		$("#correo").val(corr).show();
	});

	$("#correo").blur(function(){
		$(this).hide();
		var corr = $(this).val();
		$("#correo1").html(corr).show();
	});
        
       
    
  

});



