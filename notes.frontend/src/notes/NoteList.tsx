import {FC, ReactElement, useRef, useEffect, useState } from 'react';
import { CreateNoteDto, Client, NoteLookupDto } from '../api/api';
import { FormControl } from 'react-bootstrap';


const apiClient = new Client('https://localhost:7269/');

async function createNote(note: CreateNoteDto) {
    await apiClient.create('1.0', note);
    console.log('Note is created.');
}

const NoteList: FC<{}> = (): ReactElement => {
    let textInput = useRef(null);
    const [notes, setNotes] = useState<NoteLookupDto[] | undefined>(undefined);

    async function getNotes() {
        const noteListVm = await apiClient.getALl('1.0');
        setNotes(noteListVm.notes);
    }

    useEffect(() => {
        getNotes();
    }, []);

    const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            const note: CreateNoteDto = {
                title: event.currentTarget.value,
            };
            createNote(note);
            event.currentTarget.value = '';
            getNotes();
        }
    };

    return  (
        <div>
            Notes
            <div>
                <FormControl ref ={textInput} onKeyDown = {handleKeyPress} />
            </div>
            <div>
                {notes?.map(note => (
                    <div>{note.title}</div>
                ))}
            </div>
        </div>
    );
};
export default NoteList;
