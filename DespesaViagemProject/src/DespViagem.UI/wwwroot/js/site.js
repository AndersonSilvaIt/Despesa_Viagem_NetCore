function AjaxModal(target) {
	alert(target);
	$(document).ready(function () {
		$(function () {
			$.ajaxSetup({ cache: false });

			$("a[data-modal]").on("click",
				function (e) {
					$('#myModalContent').load(this.href,
						function () {
							$('#myModal').modal({
								keyboard: true
							},
								'show');
							bindForm(this);
						});
					return false;
				});
		});

		function bindForm(dialog) {

			$('form', dialog).submit(function () {
				$.ajax({
					url: this.action,
					type: this.method,
					data: $(this).serialize(),
					success: function (result) {
						if (result.success) {
							$('#myModal').modal('hide');
							$('#' + target).load(result.url); // Carrega o resultado HTML para a div demarcada
						} else {
							$('#myModalContent').html(result);
							bindForm(dialog);
						}
					}
				});
				return false;
			});
		}
	});
}

function BuscaCEP() {
	$(document).ready(function () {

		function limpar_formulario_cep() {
			//Limpa valores do formulário do cep
			$("#Endereco_Logradouro").val("");
			$("#Endereco_Bairro").val("");
			$("#Endereco_Cidade").val("");
			$("#Endereco_Estado").val("");
		}

		//Quando o campo cep perde o foco.
		$("#Endereco_Cep").blur(function () {

			//nova variavel "cep" somente com digitos
			var cep = $(this).val().replace(/\D/g, '');

			//verifica se campo possui valor informado.
			if (cep != "") {

				//expressao regular para validar o CEP
				var validacep = /^[0-9]{8}$/;
				if (validacep.test(cep)) {
					$("#Endereco_Logradouro").val("...");
					$("#Endereco_Bairro").val("...");
					$("#Endereco_Cidade").val("...");
					$("#Endereco_Estado").val("...");

					//Consulta o webservice viacep.com.br
					$.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
						function (dados) {
							if (!("erro" in dados)) {
								//Atualiza os campos com os valores da consulta
								$("#Endereco_Logradouro").val(dados.logradouro);
								$("#Endereco_Bairro").val(dados.bairro);
								$("#Endereco_Cidade").val(dados.localidade);
								$("#Endereco_Estado").val(dados.uf);
							} //end if
							else {
								limpar_formulario_cep();
								alert("CEP não encontrado");
							}
						});
				} // end if
				else {
					//cep é inválido
					limpar_formulario_cep();
					alert("Formato de CEP inválido");
				}
			} //endif
			else {
				//cep sem valor, limpa formulario
				limpar_formulario_cep();
			}
		});
	});
}

$(document).ready(function () {
	$("#msg_box").fadeOut(2500);
});