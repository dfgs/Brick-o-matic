import * as vscode from 'vscode';
import { BrickEditorProvider } from './BrickEditor';

export function activate(context: vscode.ExtensionContext) {
	// Register our custom editor providers
    console.log('Extension BrickEditor is now active!');

	context.subscriptions.push(BrickEditorProvider.register(context));

}
