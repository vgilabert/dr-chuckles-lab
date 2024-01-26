using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UnityCore
{
    namespace Audio
    {
        public class AudioController : MonoBehaviour
        {
            public static AudioController instance;

            public bool debug;
            public AudioSource[] Music;
            public AudioSource[] SFX;
            public static AudioController Instance
            {
                get
                {
                    if (instance == null)
                    {
                        Debug.LogError("AudioController not found!");
                    }
                    return instance;
                }
            }

            public AudioTrack[] tracks;

            private Hashtable m_AudioTable;
            private Hashtable m_JobTable;

            [System.Serializable]
            public class AudioObject
            {
                public AudioType type;
                public AudioClip clip;
                
            }
            [System.Serializable]
            public class AudioTrack
            {
                public AudioSource source;
                public AudioObject[] audio;
                public bool paused = false;
                public float volume = 1f;
                public float pitch = 0.0f;
                void Awake()
                {
                    source.volume = volume;
                    source.pitch = pitch;
                }
            }

            private class AudioJob
            {
                public AudioAction action;
                public AudioType type;
                public bool fade;
                public float delay;
                public float pitch;

                public AudioJob(AudioAction _action, AudioType _type, bool _fade, float _delay, float _pitch)
                {
                    action = _action;
                    type = _type;
                    fade = _fade;
                    delay = _delay;
                    pitch = _pitch;
                }
            }

            private enum AudioAction
            {
                START,
                STOP,
                RESTART,
                PAUSE,
            }
            
            #region Unity functions 
            private void Awake()
            {
                if (!instance)
                {
                    Configure();
                }
            }

            private void OnDisable()
            {
                Dispose();
            }

            #endregion

            #region private functions
            void Configure()
            {
                instance = this;
                DontDestroyOnLoad(this);
                m_AudioTable = new Hashtable();
                m_JobTable = new Hashtable();
                GenerateAudioTable();
                UpdateMusic();
                UpdateSFX();

            }

            void Dispose()
            {
                foreach(DictionaryEntry _entry in m_JobTable)
                {
                    IEnumerator _job = (IEnumerator)_entry.Value;
                    StopCoroutine(_job);
                }
            }

            private void GenerateAudioTable()
            {
                foreach(AudioTrack _track in tracks)
                {
                    foreach(AudioObject _obj in _track.audio)
                    {
                        if (m_AudioTable.ContainsKey(_obj.type))
                        {
                            Debug.LogWarning("you are trying to register audio [" + _obj.type + "] that has already been registered!");
                        }
                        else
                        {
                            m_AudioTable.Add(_obj.type, _track);
                            //Debug.Log("Registering audio[" + _obj.type + "].");
                        }
                    }
                }
            }

            private IEnumerator RunAudioJob(AudioJob _job)
            {
                yield return new WaitForSecondsRealtime(_job.delay);

                AudioTrack _track = (AudioTrack)m_AudioTable[_job.type];
                _track.source.clip = GetAudioClipFromAudioTrack(_job.type, _track);

                switch (_job.action)
                {
                    case AudioAction.START:
                        _track.paused = false;
                        _track.source.Play();
                        _track.source.pitch = _job.pitch;
                    break;

                    case AudioAction.STOP:
                        if (!_job.fade)
                        {
                            _track.source.Stop();
                        }
                    break;

                    case AudioAction.RESTART:
                        if (_track.paused)
                        {
                            _track.source.UnPause();
                            _track.paused = false;
                        }
                        else
                        {
                            _track.source.Stop();
                            _track.source.Play();
                        }
                        break;

                    case AudioAction.PAUSE:
                        //Debug.Log("PAUSE");
                        if (!_job.fade)
                        {
                            _track.source.Pause();
                            _track.paused = true;
                            //Debug.Log("without fading");
                        }
                        
                        break;
                }
                if (_job.fade)
                {

                    float _initial = _job.action ==AudioAction.START || _job.action == AudioAction.RESTART ? 0.0f : _track.volume;
                    float _target = _initial == 0 ? _track.volume : 0;
                    float _duration = 1.0f;
                    float _timer = 0.0f;
                    while(_timer < _duration)
                    {
                        _track.source.volume = Mathf.Lerp(_initial, _target, _timer / _duration);
                        _timer += Time.deltaTime;
                        if (_job.action == AudioAction.PAUSE)
                        {
                            //Debug.Log("with fading");
                            _track.source.Pause();
                            _track.paused = true;
                            _track.source.volume = _track.volume;
                        }
                        yield return null;
                    }
                    if (_job.action == AudioAction.PAUSE)
                    {
                        //Debug.Log("with fading");
                        _track.source.Pause();
                        _track.paused = true;
                        _track.source.volume = _track.volume;
                    }
                    if (_job.action == AudioAction.STOP)
                    {
                        _track.source.Stop();
                        _track.source.volume = _track.volume;
                    }
                }
                m_JobTable.Remove(_job.type);
                yield return null;
            }

            private void AddJob(AudioJob _job)
            {
                //remove conflicting job 
                RemoveConflictingJobs(_job.type);
                //Start job
                IEnumerator _jobRunner = RunAudioJob(_job);
                m_JobTable.Add(_job.type, _jobRunner);
                StartCoroutine(_jobRunner);
                //Debug.Log("Starting job on [" + _job.type + "] with operation: " + _job.action);
            }

            private void RemoveJob(AudioType _type)
            {
                if (!m_JobTable.ContainsKey(_type))
                {
                    //Debug.LogWarning("trying to stop a job [" + _type + "] that is not running!");
                    return;
                }
                IEnumerator _runningJob = (IEnumerator)m_JobTable[_type];
                StopCoroutine(_runningJob);
                m_JobTable.Remove(_type);
            }

            private void RemoveConflictingJobs(AudioType _type)
            {
                if (m_JobTable.ContainsKey(_type))
                {
                    RemoveJob(_type);
                }
                AudioType _conflictAudio = AudioType.None;
                foreach(DictionaryEntry _entry in m_JobTable)
                {
                    AudioType _audioType = (AudioType)_entry.Key;
                    AudioTrack _audioTrackInUse = (AudioTrack)m_AudioTable[_audioType];
                    AudioTrack _audioTrackNeeded = (AudioTrack)m_AudioTable[_type];
                    if(_audioTrackNeeded.source == _audioTrackInUse.source)
                    {
                        _conflictAudio = _audioType;
                    }
                }
                if(_conflictAudio!= AudioType.None)
                {
                    RemoveConflictingJobs(_conflictAudio);
                }
            }

            public AudioClip GetAudioClipFromAudioTrack(AudioType _type, AudioTrack _track)
            {
                foreach(AudioObject _obj in _track.audio)
                {
                    if(_obj.type == _type)
                    {
                        return _obj.clip;
                    }
                }
                return null;
            }
            #endregion

            #region public functions 

            public void PlayAudio(AudioType _type, bool _fade = false, float _delay = 0.0f, float _pitch = 1f)
            {
                AddJob(new AudioJob(AudioAction.START, _type, _fade, _delay, _pitch));
                if(debug)
                {
                    Debug.Log(_type);
                }
            }
            public void StopAudio(AudioType _type, bool _fade = false, float _delay = 0.0f, float _pitch = 1f)
            {
                AddJob(new AudioJob(AudioAction.STOP, _type, _fade, _delay, _pitch));
            }    
            public void ReplayAudio(AudioType _type, bool _fade = false, float _delay = 0.0f, float _pitch = 1f)
            {
                AddJob(new AudioJob(AudioAction.RESTART, _type, _fade, _delay, _pitch));
            }
            public void PauseAudio(AudioType _type, bool _fade = false, float _delay = 0.0f, float _pitch = 1f)
            {
                AddJob(new AudioJob(AudioAction.PAUSE, _type, _fade, _delay, _pitch));
            }

            public void UpdateMusic()
            {
                foreach (AudioSource src in Music)
                {
                    src.volume = PlayerPrefs.GetFloat("Music");
                }
            }
            public void UpdateSFX()
            {
                foreach (AudioSource src in SFX)
                {
                    src.volume = PlayerPrefs.GetFloat("SFX");
                }
            }

            #endregion
        }
    }

}
   
