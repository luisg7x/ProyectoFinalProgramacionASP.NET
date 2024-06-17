/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
$(document).ready(main);

var contador =1;


function main(){
    //menu movil
	$('.menu_bar').click(function(){
 
		if(contador == 1){
			$('nav').animate({
				left: '0'
			});
			contador = 0;
		} else {
			contador = 1;
			$('nav').animate({
				left: '-100%'
			});
		}
 
	});
 


    //submenu movil
            $('.submenu').click(function(){
		$(this).children('.hijos').slideToggle();
 
            });
}

