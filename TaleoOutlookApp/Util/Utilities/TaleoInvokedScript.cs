using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Utilities
{
    public static class TaleoInvokedScript
    {
        /*******Taleo PopUp Removal Script******/
        public const string PopUpRemovalScript = @"var removePopUpScreen=  setInterval  (   function()  {  
                                                    if($('#outlookControlDialog')){
                                                        if($('#outlookControlDialog').is(':visible')){
                                                            $('#outlookControlDialog').dialog('close');
                                                            clearInterval(removePopUpScreen);
                                                        }
                                                    }else{
                                                        clearInterval(removePopUpScreen);
                                                    }
                                               }, 1000);";
        /****Set Window Load Null**********/
        public const string SetWindowLoadNull = @"function setWindowLoadNull() {
                                                                     window.onload = null;
                                                                     $('OBJECT').removeAttr('CODEBASE');
                                                                     $( ""[event*='onload']"" ).html('');
                                                                 }";
        /*******File FeedBack Invoke Script***********/
        public const string FileFeedBackOnclickRemovalScript = @"function removeFileFeedBackOnclick() {
                                                                 window.onload = null;
                                                                 $('OBJECT').removeAttr('CODEBASE');
                                                                 $( ""[event*='onload']"" ).html('');
                                                                $('#EMSEL_0 .DTRC').each(function () {
                                                                    var onclickValue= $(this).find('a').attr('onclick');
			                                                        $(this).find('a').removeAttr('onclick');
				                                                    $(this).find('a').attr('customAttribute',onclickValue);
                                                                });
                                                          }";
        /***********************/
        public const string AddressBookOnclickRemovalScript = @"function removeOnclickAddressBook() {
                                                                 window.onload = null;
                                                                 $('OBJECT').removeAttr('CODEBASE');
                                                                 $( ""[event*='onload']"" ).html('');
                                                                $('#CASEL_0 .DTRC').each(function () {
                                                                    var onclickValue = $(this).find('a').attr('onclick');
                                                                    $(this).find('a').removeAttr('onclick');
                                                                    $(this).find('a').attr('hello', onclickValue);
                                                                });
                                                               }";
     public const string AddressBookContactOnclickRemovalScript = @"function removeOnclickAddressBook() {
                                                                 window.onload = null;
                                                                 $('OBJECT').removeAttr('CODEBASE');
                                                                 $( ""[event*='onload']"" ).html('');
                                                                $('#CTSEL_0 .DTRC').each(function () {
                                                                    var onclickValue = $(this).find('a').attr('onclick');
                                                                    $(this).find('a').removeAttr('onclick');
                                                                    $(this).find('a').attr('hello', onclickValue);
                                                                });
                                                               }";
        /*********AddToTaleoWithAttachment***********/
        public const string AddToTaleoWithAttachmentInvokeHiddenFieldScript = @"function addHiddenField() {
                                                                                     window.onload = null;
                                                                                     $('OBJECT').removeAttr('CODEBASE');
                                                                                     $( ""[event*='onload']"" ).html('');
                                                                                if(document.theForm){
                                                                                  document.theForm.innerHTML+= ""<input id='theNameSubmit' type='hidden' value='' />"";
                                                                                  $('#theNameSubmit').off('click').on('click', function (){
                                                                                                    document.theForm.submit();
                                                                             });
                                                                          }
                                                                       }";
        /*********AddToTaleo***********/
        public const string AddToTaleoInvokeSearchCandidateInvokedScript = @"function searchCandidateWithEmail(email){
                                                                                window.onload = null;
                                                                             $('OBJECT').removeAttr('CODEBASE');
                                                                             $( ""[event*='onload']"" ).html('');
                                                                                $(""input[name=keywords]"").val(email);
                                                                                $(""form[name=candSelectForm]"").submit();
                                                                        }";

        public const string AddToTaleoNewContactSumitScript = @"function newContactSumit() {
                                                                                     $('form[name=theForm]').submit();
                                                                          }
                                                                       }";
    }
}
