/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	
	// %REMOVE_START%
	// The configuration options below are needed when running CKEditor from source files.
	config.plugins = 'dialogui,dialog,about,a11yhelp,dialogadvtab,basicstyles,bidi,blockquote,clipboard,button,panelbutton,panel,floatpanel,colorbutton,colordialog,templates,menu,contextmenu,copyformatting,div,resize,toolbar,elementspath,enterkey,entities,popup,filebrowser,find,fakeobjects,flash,floatingspace,listblock,richcombo,font,forms,format,horizontalrule,htmlwriter,iframe,wysiwygarea,image,indent,indentblock,indentlist,smiley,justify,menubutton,language,link,list,liststyle,magicline,maximize,newpage,pagebreak,pastetext,pastefromword,preview,print,removeformat,save,selectall,showblocks,showborders,sourcearea,specialchar,scayt,stylescombo,tab,table,tabletools,undo,wsc';
	config.skin = 'bootstrapck';
	// %REMOVE_END%

	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	 config.uiColor = '#F7B42C';
	config.height = 300;
	config.toolbarCanCollapse = true;
	config.colorButton_backStyle = {
    element: 'span',
    styles: { 'background-color': '#(color)' }
};
	config.extraPlugins = 'colorbutton';
	config.enterMode = CKEDITOR.ENTER_BR; // pressing the ENTER KEY input <br/>
	config.shiftEnterMode = CKEDITOR.ENTER_P; //pressing the SHIFT + ENTER KEYS input <p>
	//config.forcePasteAsPlainText = false;
	//config.pasteFromWordRemoveFontStyles = false;
	//config.pasteFromWordRemoveStyles = false;
	//config.allowedContent = true;
	//config.extraAllowedContent = 'p(mso*,Normal)';
	//config.pasteFilter = null;
};
