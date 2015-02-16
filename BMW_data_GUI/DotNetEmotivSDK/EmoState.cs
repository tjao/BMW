using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emotiv
{
    /// <summary>
    /// EmoStates are generated by the Emotiv detection engine (EmoEngine) and represent
    /// the emotional status of the user at a given time.
    /// </summary>
    public class EmoState : ICloneable
    {
        private IntPtr hEmoState;       
       
        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="es">EmoState to be copied</param>
        public EmoState(EmoState es)
        {
            this.hEmoState = EdkDll.ES_Create();
            EdkDll.ES_Copy(this.hEmoState, es.GetHandle());
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public EmoState()
        {
            hEmoState = EdkDll.ES_Create();
        }
        /// <summary>
        /// Destructor
        /// </summary>
        ~EmoState()
        {
            if (hEmoState != null)
                EdkDll.ES_Free(hEmoState);
        }
        /// <summary>
        /// Retuns handle which is used to represent the internal structure of EmoState in the unmanged dll
        /// </summary>
        /// <returns>Internal structure of EmoState in the unmanged dll</returns>
        public IntPtr GetHandle()
        {
            return hEmoState;
        }
        /// <summary>
        /// Clones the current EmoState
        /// </summary>
        /// <returns>Copy of the current EmoState</returns>
        public Object Clone(){
            return new EmoState(this);
        }
        /// <summary>
        /// Return the time since EmoEngine has been successfully connected to the headset
        /// </summary>
        /// <remarks>
        /// If the headset is disconnected from EmoEngine due to low battery or weak
		/// wireless signal, the time will be reset to zero.
        /// </remarks>
        /// <returns>time in second</returns>
        public Single GetTimeFromStart()
        {
            return EdkDll.ES_GetTimeFromStart(hEmoState);
        }
        /// <summary>
        /// Return whether the headset has been put on correctly or not
        /// </summary>
        /// <remarks>
        /// If the headset cannot not be detected on the head, then signal quality will not report
		/// any results for all the channels
        /// </remarks>
        /// <returns>int (1: On, 0: Off)</returns>
        public Int32 GetHeadsetOn()
        {
            return EdkDll.ES_GetHeadsetOn(hEmoState);
        }
        /// <summary>
        /// Query the number of channels of available sensor contact quality data
        /// </summary>
        /// <returns>number of channels for which contact quality data is available</returns>
        public Int32 GetNumContactQualityChannels()
        {
            return EdkDll.ES_GetNumContactQualityChannels(hEmoState);
        }
        /// <summary>
        /// Query the contact quality of a specific EEG electrode
        /// </summary>
        /// <param name="electroIdx">The index of the electrode for query</param>
        /// <returns>Enumerated value that characterizes the EEG electrode contact for the specified input channel</returns>
        public EdkDll.EE_EEG_ContactQuality_t GetContactQuality(Int32 electroIdx)
        {
            return EdkDll.ES_GetContactQuality(hEmoState, electroIdx);
        }
        /// <summary>
        /// Query the contact quality of all the electrodes in one single call
        /// </summary>
        /// <remarks>
        /// The contact quality will be stored in the array, contactQuality, passed to the function.
		/// The value stored in contactQuality[0] is identical to the result returned by
		/// ES_GetContactQuality(state, 0)
		/// The value stored in contactQuality[1] is identical to the result returned by
		/// ES_GetContactQuality(state, 1). etc.
		/// The ordering of the array is consistent with the ordering of the logical input
        /// channels in EE_InputChannels_enum.
        /// </remarks>
        /// <returns>Number of signal quality values copied to the contactQuality array.</returns>
        public EdkDll.EE_EEG_ContactQuality_t[] GetContactQualityFromAllChannels()
        {
            EdkDll.EE_EEG_ContactQuality_t[] contactQuality;
            EdkDll.ES_GetContactQualityFromAllChannels(hEmoState, out contactQuality);
            return contactQuality;
        }
        /// <summary>
        /// Query whether the user is blinking at the time the EmoState is captured.
        /// </summary>
        /// <returns>blink status (true: blink, false: not blink)</returns>
        public Boolean ExpressivIsBlink()
        {
            return EdkDll.ES_ExpressivIsBlink(hEmoState);
        }
        /// <summary>
        /// Query whether the user is winking left at the time the EmoState is captured.
        /// </summary>
        /// <returns>left wink status (true: wink, false: not wink)</returns>
        public Boolean ExpressivIsLeftWink()
        {
            return EdkDll.ES_ExpressivIsLeftWink(hEmoState);
        }
        /// <summary>
        /// Query whether the user is winking right at the time the EmoState is captured.
        /// </summary>
        /// <returns>right wink status (true: wink, false: not wink)</returns>
        public Boolean ExpressivIsRightWink()
        {
            return EdkDll.ES_ExpressivIsRightWink(hEmoState);
        }
        /// <summary>
        /// Query whether the eyes of the user are opened at the time the EmoState is captured.
        /// </summary>
        /// <returns>eye open status (true: eyes open, false: eyes closed)</returns>
        public Boolean ExpressivIsEyesOpen()
        {
            return EdkDll.ES_ExpressivIsEyesOpen(hEmoState);
        }
        /// <summary>
        /// Query whether the user is looking up at the time the EmoState is captured.
        /// </summary>
        /// <returns>eyes position (true: looking up, false: not looking up)</returns>
        public Boolean ExpressivIsLookingUp()
        {
            return EdkDll.ES_ExpressivIsLookingUp(hEmoState);
        }
        /// <summary>
        /// Query whether the user is looking down at the time the EmoState is captured.
        /// </summary>
        /// <returns>eyes position (true: looking down, false: not looking down)</returns>
        public Boolean ExpressivIsLookingDown()
        {
            return EdkDll.ES_ExpressivIsLookingDown(hEmoState);
        }
        /// <summary>
        /// Query whether the user is looking left at the time the EmoState is captured.
        /// </summary>
        /// <returns>eye position (true: looking left, false: not looking left)</returns>
        public Boolean ExpressivIsLookingLeft()
        {
            return EdkDll.ES_ExpressivIsLookingLeft(hEmoState);
        }
        /// <summary>
        /// Query whether the user is looking right at the time the EmoState is captured.
        /// </summary>
        /// <returns>eye position (true: looking right, false: not looking right)</returns>
        public Boolean ExpressivIsLookingRight()
        {
            return EdkDll.ES_ExpressivIsLookingRight(hEmoState);
        }

        /// <summary>
        /// Query the eyelids state of the user
        /// </summary>
        /// <remarks>
        /// The left and right eyelid state are stored in the parameter leftEye and rightEye
		/// respectively. They are floating point values ranging from 0.0 to 1.0.
		/// 0.0 indicates that the eyelid is fully opened while 1.0 indicates that the
        /// eyelid is fully closed.
        /// </remarks>
        /// <param name="leftEye">the left eyelid state (0.0 to 1.0)</param>
        /// <param name="rightEye">the right eyelid state (0.0 to 1.0)</param>
        public void ExpressivGetEyelidState(out Single leftEye, out Single rightEye)
        {
            EdkDll.ES_ExpressivGetEyelidState(hEmoState, out leftEye, out rightEye);
        }
        /// <summary>
        /// Query the eyes position of the user
        /// </summary>
        /// <remarks>
        /// The horizontal and vertical position of the eyes are stored in the parameter x and y
		/// respectively. They are floating point values ranging from -1.0 to 1.0.
		/// 
		/// For horizontal position, -1.0 indicates that the user is looking left while
		/// 1.0 indicates that the user is looking right.
		/// 
		/// For vertical position, -1.0 indicates that the user is looking down while
		/// 1.0 indicatest that the user is looking up.
        /// 
		/// This function assumes that both eyes have the same horizontal or vertical positions.
        /// (i.e. no cross eyes)
        /// </remarks>
        /// <param name="x">the horizontal position of the eyes</param>
        /// <param name="y">the veritcal position of the eyes</param>
        public void ExpressivGetEyeLocation(out Single x, out Single y)
        {
            EdkDll.ES_ExpressivGetEyeLocation(hEmoState, out x, out y);
        }
        /// <summary>
        /// Returns the eyebrow extent of the user (Obsolete function)
        /// </summary>
        /// <returns>eyebrow extent value (0.0 to 1.0)</returns>
        public Single ExpressivGetEyebrowExtent()
        {
            return EdkDll.ES_ExpressivGetEyebrowExtent(hEmoState);
        }

        /// <summary>
        /// Returns the smile extent of the user (Obsolete function)
        /// </summary>
        /// <returns>smile extent value (0.0 to 1.0)</returns>
        public Single ExpressivGetSmileExtent()
        {
            return EdkDll.ES_ExpressivGetSmileExtent(hEmoState);
        }
        /// <summary>
        /// Returns the clench extent of the user (Obsolete function)
        /// </summary>
        /// <returns>clench extent value (0.0 to 1.0)</returns>
        public Single ExpressivGetClenchExtent()
        {
            return EdkDll.ES_ExpressivGetClenchExtent(hEmoState);
        }
        /// <summary>
        /// Returns the detected upper face Expressiv action of the user
        /// </summary>
        /// <returns>pre-defined Expressiv action types</returns>
        public EdkDll.EE_ExpressivAlgo_t ExpressivGetUpperFaceAction()
        {
            return EdkDll.ES_ExpressivGetUpperFaceAction(hEmoState);
        }
        /// <summary>
        /// Returns the detected upper face Expressiv action power of the user
        /// </summary>
        /// <returns>power value (0.0 to 1.0)</returns>
        public Single ExpressivGetUpperFaceActionPower()
        {
            return EdkDll.ES_ExpressivGetUpperFaceActionPower(hEmoState);
        }
        /// <summary>
        /// Returns the detected lower face Expressiv action of the user
        /// </summary>
        /// <returns>pre-defined Expressiv action types</returns>
        public EdkDll.EE_ExpressivAlgo_t ExpressivGetLowerFaceAction()
        {
            return EdkDll.ES_ExpressivGetLowerFaceAction(hEmoState);
        }
        /// <summary>
        /// Returns the detected lower face Expressiv action power of the user
        /// </summary>
        /// <returns>power value (0.0 to 1.0)</returns>
        public Single ExpressivGetLowerFaceActionPower()
        {
            return EdkDll.ES_ExpressivGetLowerFaceActionPower(hEmoState);
        }
        /// <summary>
        /// Query whether the signal is too noisy for Expressiv detection to be active
        /// </summary>
        /// <param name="type">Expressiv detection type</param>
        /// <returns>detection state (false: Not Active, true: Active)</returns>
        public Boolean ExpressivIsActive(EdkDll.EE_ExpressivAlgo_t type)
        {
            return EdkDll.ES_ExpressivIsActive(hEmoState, type);
        }
        /// <summary>
        /// Returns the long term excitement level of the user
        /// </summary>
        /// <returns>excitement level (0.0 to 1.0)</returns>
        public Single AffectivGetExcitementLongTermScore()
        {
            return EdkDll.ES_AffectivGetExcitementLongTermScore(hEmoState);
        }
        /// <summary>
        /// Returns short term excitement level of the user
        /// </summary>
        /// <returns>excitement level (0.0 to 1.0)</returns>
        public Single AffectivGetExcitementShortTermScore()
        {
            return EdkDll.ES_AffectivGetExcitementShortTermScore(hEmoState);
        }
        /// <summary>
        /// Query whether the signal is too noisy for Affectiv detection to be active
        /// </summary>
        /// <param name="type">Affectiv detection type</param>
        /// <returns>detection state (false: Not Active, true: Active)</returns>
        public Boolean AffectivIsActive(EdkDll.EE_AffectivAlgo_t type)
        {
            return EdkDll.ES_AffectivIsActive(hEmoState, type);
        }
        /// <summary>
        /// Returns meditation level of the user
        /// </summary>
        /// <returns>meditation level (0.0 to 1.0)</returns>
        public Single AffectivGetMeditationScore()
        {
            return EdkDll.ES_AffectivGetMeditationScore(hEmoState);
        }
        /// <summary>
        /// Returns frustration level of the user
        /// </summary>
        /// <returns>frustration level (0.0 to 1.0)</returns>
        public Single AffectivGetFrustrationScore()
        {
            return EdkDll.ES_AffectivGetFrustrationScore(hEmoState);
        }
        /// <summary>
        /// Returns engagement/boredom level of the user
        /// </summary>
        /// <returns>engagement/boredom level (0.0 to 1.0)</returns>
        public Single AffectivGetEngagementBoredomScore()
        {
            return EdkDll.ES_AffectivGetEngagementBoredomScore(hEmoState);
        }
        /// <summary>
        /// Returns the detected Cognitiv action of the user
        /// </summary>
        /// <returns>Cognitiv action type</returns>
        public EdkDll.EE_CognitivAction_t CognitivGetCurrentAction()
        {
            return EdkDll.ES_CognitivGetCurrentAction(hEmoState);
        }
        /// <summary>
        /// Returns the detected Cognitiv action power of the user
        /// </summary>
        /// <returns>Cognitiv action power (0.0 to 1.0)</returns>
        public Single CognitivGetCurrentActionPower()
        {
            return EdkDll.ES_CognitivGetCurrentActionPower(hEmoState);
        }
        /// <summary>
        /// Query whether the signal is too noisy for Cognitiv detection to be active
        /// </summary>
        /// <returns>detection state (false: Not Active, true: Active)</returns>
        public Boolean CognitivIsActive()
        {
            return EdkDll.ES_CognitivIsActive(hEmoState);
        }
        /// <summary>
        /// Query of the current wireless signal strength
        /// </summary>
        /// <returns>
        /// wireless signal strength [No Signal, Bad, Fair, Good, Excellent].
        /// </returns>
        public EdkDll.EE_SignalStrength_t GetWirelessSignalStatus()
        {
            return EdkDll.ES_GetWirelessSignalStatus(hEmoState);
        }
        /// <summary>
        /// Get the level of charge remaining in the headset battery
        /// </summary>
        /// <param name="chargeLevel">the current level of charge in the headset battery</param>
        /// <param name="maxChargeLevel">the maximum level of charge in the battery</param>
        public void GetBatteryChargeLevel(out Int32 chargeLevel, out Int32 maxChargeLevel)
        {
            EdkDll.ES_GetBatteryChargeLevel(hEmoState, out chargeLevel, out maxChargeLevel);
        }
        /// <summary>
        /// Check whether two states are with identical 'emotiv' state
        /// </summary>
        /// <param name="state">EmoState</param>
        /// <returns>true: Equal, false: Different</returns>
        public Boolean AffectivEqual(EmoState state)
        {
            return EdkDll.ES_AffectivEqual(GetHandle(), state.GetHandle());
        }
        /// <summary>
        /// Check whether two states are with identical Expressiv state, i.e. are both state representing the same facial expression
        /// </summary>
        /// <param name="state">EmoState</param>
        /// <returns>true: Equal, false: Different</returns>
        public Boolean ExpressivEqual(EmoState state)
        {
            return EdkDll.ES_ExpressivEqual(GetHandle(), state.GetHandle());
        }
        /// <summary>
        /// Check whether two states are with identical Cognitiv state
        /// </summary>
        /// <param name="state">EmoState</param>
        /// <returns>true: Equal, false: Different</returns>
        public Boolean CognitivEqual(EmoState state)
        {
            return EdkDll.ES_CognitivEqual(GetHandle(), state.GetHandle());
        }
        /// <summary>
        /// Check whether two states are with identical EmoEngine state.
        /// </summary>
        /// <remarks>
        /// This function is comparing the time since EmoEngine start,
		/// the wireless signal strength and the signal quality of different channels
        /// </remarks>
        /// <param name="state">EmoState</param>
        /// <returns>true: Equal, false: Different</returns>
        public Boolean EmoEngineEqual(EmoState state)
        {
            return EdkDll.ES_EmoEngineEqual(GetHandle(), state.GetHandle());
        }
        /// <summary>
        /// Check whether two EmoStateHandles are identical
        /// </summary>
        /// <param name="a">EmoState</param>
        /// <param name="b">EmoState</param>
        /// <returns></returns>
        public Boolean Equals (EmoState a, EmoState b)
        {
            return EdkDll.ES_Equal(a.GetHandle(), b.GetHandle());
        }
      /// <summary>
        /// Returns short term excitement model parameters
        /// </summary>
        /// <param name="rawScore"> return raw score</param>
        /// <param name="minScale,maxScale"> return scale range</param>
        /// <returns></returns>
        public void AffectivGetExcitementShortTermModelParams(out Double rawScore, out Double minScale, out Double maxScale)
        {
            EdkDll.ES_AffectivGetExcitementShortTermModelParams(hEmoState, out rawScore, out minScale, out maxScale);
        }
        /// <summary>
        /// Returns Meditation model parameters
        /// </summary>
        /// <param name="rawScore"> return raw score</param>
        /// <param name="minScale,maxScale"> return scale range</param>
        /// <returns></returns>
      
        public void AffectivGetMeditationModelParams(out Double rawScore, out Double minScale, out Double maxScale)
        {
            EdkDll.ES_AffectivGetMeditationModelParams(hEmoState, out rawScore, out minScale, out maxScale);
        }
        /// <summary>
        /// Returns EngagementBoredom model parameters
        /// </summary>
        /// <param name="rawScore"> return raw score</param>
        /// <param name="minScale,maxScale"> return scale range</param>
        /// <returns></returns>
        public void AffectivGetEngagementBoredomModelParams(out Double rawScore, out Double minScale, out Double maxScale)
        {
            EdkDll.ES_AffectivGetEngagementBoredomModelParams(hEmoState, out rawScore, out minScale, out maxScale);
        }
        /// <summary>
        ///  Returns Frustration model parameters
        /// </summary>
        /// <param name="rawScore"> return raw score</param>
        /// <param name="minScale,maxScale"> return scale range</param>
        /// <returns></returns>
        public void AffectivGetFrustrationModelParams(out Double rawScore, out Double minScale, out Double maxScale)
        {
            EdkDll.ES_AffectivGetFrustrationModelParams(hEmoState, out rawScore, out minScale, out maxScale);
        }
        /// <summary>
        ///  Returns Valence model parameters
        /// </summary>
        /// <param name="rawScore"> return raw score</param>
        /// <param name="minScale,maxScale"> return scale range</param>
        /// <returns></returns>
    }
}